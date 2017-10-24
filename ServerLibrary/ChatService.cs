using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace ServerLibrary
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,
        InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = false)]
    public class ChatService : IChatService
    {
        #region Constants

        private static readonly string AVATAR_LOCATION = @"Resources/Avatar/";

        #endregion

        #region Variables

        private string _strConnection;

        object _synObj = new object();

        Dictionary<User, IChatServiceCallback> _onlineUsers
            = new Dictionary<User, IChatServiceCallback>(new UserEqualityComparer());

        List<User> _allUsers = new List<User>();

        Dictionary<User, NodeRelationship> _relationship =
            new Dictionary<User, NodeRelationship>(new UserEqualityComparer());

        #endregion

        public IChatServiceCallback GetCurrentCallback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IChatServiceCallback>();
            }
        }

        public ChatService()
        {
            _strConnection = GetConnectionString();
        }

        #region private methods

        /// <summary>
        /// Get connection string from ConfigurationManager
        /// </summary>
        /// <returns>Connection string</returns>
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["zola"].ConnectionString;
        }

        /// <summary>
        /// Load all users from database into _allUserss
        /// </summary>
        private void Init()
        {
            //Init _allUsers
            String query = "SELECT username, name, gender " +
                "FROM Users";
            using (SqlConnection connection = new SqlConnection(_strConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User()
                    {
                        IsMale = (bool)reader["gender"],
                        IsOnline = false,
                        Name = (string)reader["name"],
                        Username = (string)reader["username"]
                    };
                    _allUsers.Add(user);
                    _relationship.Add(user, new NodeRelationship());
                }
            }

            //Init _relationship
            query = "SELECT foo, bar, relation " +
                "FROM Relations";
            using (SqlConnection connection = new SqlConnection(_strConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string foo = reader["foo"].ToString();
                    string bar = reader["bar"].ToString();
                    int relationType = (int)reader["relation"];
                    MakeFriend(foo, bar);
                }
            }

            //Init directory
            DirectoryInfo dir = new DirectoryInfo(AVATAR_LOCATION);
            if (!dir.Exists) dir.Create();
        }

        /// <summary>
        /// Make friend relationship between 2 users which
        /// username is foo and bar.
        /// </summary>
        /// <param name="foo"></param>
        /// <param name="bar"></param>
        private void MakeFriend(string foo, string bar)
        {
            User fooUser = new User() { Username = foo };
            User barUser = new User() { Username = bar };
            _relationship[fooUser].Friends.Add(_relationship[barUser]);
            _relationship[barUser].Friends.Add(_relationship[fooUser]);
        }

        /// <summary>
        /// Add Online User
        /// If complete, notify all online friends of this user
        /// if online friends list suddenly change, 
        /// notify all users to update new online list
        /// </summary>
        /// <param name="username"></param>
        private void AddUserOnline(string username)
        {
            bool nothingCorrupt = true;
            lock (_synObj)
            {

                User curUser = _allUsers.Find(x => x.Username == username);
                if (curUser.IsOnline == false)
                {
                    curUser.IsOnline = true;
                    _onlineUsers.Add(curUser, GetCurrentCallback);
                    foreach (NodeRelationship nodeFriend in _relationship[curUser].Friends)
                    {
                        User friend = nodeFriend.User;
                        if (friend.IsOnline == false) continue;
                        try
                        {
                            _onlineUsers[friend].FriendOnline(curUser);
                        }
                        catch
                        {
                            nothingCorrupt = false;
                            friend.IsOnline = false;
                            _onlineUsers.Remove(friend);
                        }
                    }
                }
            }

            if (!nothingCorrupt)
            {
                foreach (IChatServiceCallback callback in _onlineUsers.Values)
                {
                    callback.FriendOnlineListChangeUnexpectedly();
                }
            }
        }
        #endregion


        #region Implement IChatService
        public bool Login(string username, string password)
        {
            bool res = false;
            string query = "SELECT * FROM Users " +
                "WHERE username = @username and password = @password";
            using (SqlConnection connection = new SqlConnection(_strConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                res = reader.HasRows;
            }

            if (res)
            {
                //add to online users
                AddUserOnline(username);
            }

            return res;
        }

        public bool Register(string username, string password, string name)
        {
            string query = "INSERT INTO Users (username, password, name) " +
                "VALUES (@username, @password, @name)";
            using (SqlConnection connection = new SqlConnection(_strConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@name", name);

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                return command.ExecuteNonQuery() == 1;
            }
        }

        public List<User> GetFriends(string username)
        {
            List<User> friends = new List<User>();
            lock (_synObj)
            {
                User curUser = _allUsers.Find(x => x.Username == username);
                foreach (NodeRelationship node in _relationship[curUser].Friends)
                {
                    friends.Add(node.User);
                }
            }
            return friends;
        }

        public DataFile GetAvatarFile(string username)
        {
            DataFile result = new DataFile
            {
                FileName = username,
                Data = File.ReadAllBytes(AVATAR_LOCATION + username)
            };
            return result;
        }

        public bool IsUserHasAvatar(string username)
        {
            FileInfo file = new FileInfo(AVATAR_LOCATION + username);
            if (file.Exists) return true;
            return false;
        }

        public List<User> FindPeople(string username)
        {
            List<User> result = new List<User>();
            if(username.Length > 0)
            {
                result = _allUsers.FindAll(x => x.Username.Contains(username));
            }
            return result;
        }

        #endregion
    }
}
