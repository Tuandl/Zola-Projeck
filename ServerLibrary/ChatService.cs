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

        private static readonly string AVATAR_LOCATION = Environment.CurrentDirectory + @"\Resources\Avatar\";
        private static readonly string[] SUPPORT_AVATAR_EXTENSIONS = { "png", "jpg", "jpeg" };

        #endregion

        #region Variables

        private static string _strConnection;

        private static object _synObj = new object();
        private static object _synNotReceivedMessage = new object();

        private static Dictionary<User, IChatServiceCallback> _onlineUsers
            = new Dictionary<User, IChatServiceCallback>(new UserEqualityComparer());

        private static List<User> _allUsers = new List<User>();

        private static Dictionary<User, NodeRelationship> _relationship =
            new Dictionary<User, NodeRelationship>(new UserEqualityComparer());

        private static List<DataMessage> _notReceivedMessages = new List<DataMessage>();

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
        /// Load relationship of users
        /// Create directory for storing avatar image
        /// Load unreceived messages
        /// </summary>
        public static void Init()
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
                        IsOnline = false,
                        Name = (string)reader["name"],
                        Username = (string)reader["username"]
                    };
                    if (reader["gender"].ToString().Length > 0)
                    {
                        user.IsMale = (bool)reader["gender"];
                    }
                    _allUsers.Add(user);
                    _relationship.Add(user, new NodeRelationship(user));
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
                    if (relationType == (int)RelationshipType.Friend)
                    {
                        User fooUser = _allUsers.Find(x => x.Username == foo);
                        User barUser = _allUsers.Find(x => x.Username == bar);
                        _relationship[fooUser].Friends.Add(_relationship[barUser]);
                        _relationship[barUser].Friends.Add(_relationship[fooUser]);
                    }
                    else if (relationType == (int)RelationshipType.Pending)
                    {
                        User fooUser = _allUsers.Find(x => x.Username == foo);
                        User barUser = _allUsers.Find(x => x.Username == bar);
                        _relationship[fooUser].SentPendingRequest.Add(_relationship[barUser]);
                        _relationship[barUser].ReceivedPendingRequest.Add(_relationship[fooUser]);
                    }
                }
            }

            //Init directory
            DirectoryInfo dir = new DirectoryInfo(AVATAR_LOCATION);
            if (!dir.Exists) dir.Create();

            //load unreceived messages
            query = "SELECT id, sender, receiver, time_sent, message " +
                "FROM Messages " +
                "WHERE status = @NotReceivedStatus";
            using (SqlConnection connection = new SqlConnection(_strConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NotReceivedStatus", (int)DataMessageStatus.Sent);

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DataMessage msg = new DataMessage()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Message = (string)reader["message"],
                        Receiver = _allUsers.Find(x => x.Username == (string)reader["receiver"]),
                        Sender = _allUsers.Find(x => x.Username == (string)reader["sender"]),
                        SentTime = (DateTime)reader["time_sent"]
                    };
                    _notReceivedMessages.Add(msg);
                }
            }
        }

        /// <summary>
        /// Foo sent make friend request to bar
        /// This function connect relationship between fooUser and barUser
        /// </summary>
        /// <param name="fooUser"></param>
        /// <param name="barUser"></param>
        private void MakePendingRequest(User fooUser, User barUser)
        {
            _relationship[fooUser].SentPendingRequest.Add(_relationship[barUser]);
            _relationship[barUser].ReceivedPendingRequest.Add(_relationship[fooUser]);
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

        /// <summary>
        /// Remove Online user, if complete notify all online friends of this user
        /// If online friends list suddenly change, notify callback all friend to update.
        /// </summary>
        /// <param name="username"></param>
        private void RemoveUserOnline(string username)
        {
            bool nothingCorrupt = true;
            lock (_synObj)
            {

                User curUser = _allUsers.Find(x => x.Username == username);
                if (curUser.IsOnline == true)
                {
                    curUser.IsOnline = false;
                    _onlineUsers.Remove(curUser);
                    foreach (NodeRelationship nodeFriend in _relationship[curUser].Friends)
                    {
                        User friend = nodeFriend.User;
                        if (friend.IsOnline == false) continue;
                        try
                        {
                            _onlineUsers[friend].FriendOffline(curUser);
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

        /// <summary>
        /// Insert message into database
        /// return message id that automatic genarated
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private int AddMessageIntoDatabase(DataMessage msg)
        {
            int result = -1;
            string query = "INSERT INTO Messages (sender, receiver, time_sent, [message], [status]) " +
                "OUTPUT inserted.id " +
                "VALUES(@sender, @receiver, @time_sent, @message, @status)";
            using (SqlConnection connection = new SqlConnection(_strConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@sender", msg.Sender.Username);
                command.Parameters.AddWithValue("@receiver", msg.Receiver.Username);
                command.Parameters.AddWithValue("@time_sent", msg.SentTime);
                command.Parameters.AddWithValue("@message", msg.Message);
                command.Parameters.AddWithValue("@status", (int)DataMessageStatus.Sent);

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();
                result = int.Parse(command.ExecuteScalar().ToString());
            }
            return result;
        }

        /// <summary>
        /// Change Message status into Received
        /// Whose Id = message.id
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool UpdateReceivedMessageIntoDatabase(DataMessage message)
        {
            bool res = false;

            string query = "UPDATE Messages " +
                "SET [status] = @status " +
                "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_strConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@status", (int)DataMessageStatus.Received);
                command.Parameters.AddWithValue("@id", message.Id);

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();
                res = 1 == command.ExecuteNonQuery();
            }

            return res;
        }

        /// <summary>
        /// Update name, gender of a user
        /// whose username == user.username
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool UpdateUserInformation(User user)
        {
            bool res = false;
            string query = "UPDATE Users " +
                "SET name = @name, " +
                "gender = @gender " +
                "WHERE username = @username";
            using (SqlConnection connection = new SqlConnection(_strConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@gender", user.IsMale);
                command.Parameters.AddWithValue("@username", user.Username);

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                res = command.ExecuteNonQuery() == 1;
            }
            return res;
        }

        private bool InsertPendingRequestIntoDatabase(string foo, string bar)
        {
            bool res = false;
            string query = "INSERT INTO Relations (foo, bar, relation) " +
                "VALUES (@foo, @bar, @relation)";
            using (SqlConnection connection = new SqlConnection(_strConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@foo", foo);
                command.Parameters.AddWithValue("@bar", bar);
                command.Parameters.AddWithValue("@relation", (int)RelationshipType.Pending);

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                res = command.ExecuteNonQuery() == 1;
            }
            return res;
        }

        private bool UpdateFriendshipIntoDatabase(string foo, string bar)
        {
            bool res = false;
            string query = "UPDATE Relations " +
                "SET relation = @relationType " +
                "WHERE foo = @foo AND bar = @bar ";
            using (SqlConnection connection = new SqlConnection(_strConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@relationType", (int)RelationshipType.Friend);
                command.Parameters.AddWithValue("@foo", foo);
                command.Parameters.AddWithValue("@bar", bar);

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                res = command.ExecuteNonQuery() == 1;
            }
            return res;
        }

        #endregion


        #region Implement IChatService
        public bool Login(string username, string password)
        {
            Console.WriteLine("call login service params: username = " + username + "; password = " + password);
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
            Console.WriteLine("Exit login service");
            return res;
        }

        public bool Register(string username, string password, string name)
        {
            Console.WriteLine("call register service params: username = " + username + "; password = " + password + "; name = " + name);
            bool registerComplete = false;
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

                registerComplete = command.ExecuteNonQuery() == 1;
            }

            if (registerComplete)
            {
                User newUser = new User()
                {
                    Username = username,
                    Name = name,
                    IsMale = null,
                    IsOnline = false
                };
                _allUsers.Add(newUser);
                _relationship.Add(newUser, new NodeRelationship(newUser));
            }

            Console.WriteLine("Exit register service");
            return registerComplete;
        }

        public List<User> GetFriends(string username)
        {
            Console.WriteLine("Call get friends service params: username = " + username);
            List<User> friends = new List<User>();
            lock (_synObj)
            {
                User curUser = _allUsers.Find(x => x.Username == username);
                foreach (NodeRelationship node in _relationship[curUser].Friends)
                {
                    friends.Add(node.User);
                }
            }
            Console.WriteLine("exit get friends service");
            return friends;
        }

        public DataFile GetAvatarFile(string username)
        {
            Console.WriteLine("call get avatar file params: username = " + username);
            FileInfo file;
            DataFile result = null;
            for (int i = 0; i < SUPPORT_AVATAR_EXTENSIONS.Length; i++)
            {
                file = new FileInfo(AVATAR_LOCATION + username + "." + SUPPORT_AVATAR_EXTENSIONS[i]);
                if (file.Exists)
                {
                    result = new DataFile
                    {
                        FileName = file.Name,
                        Data = File.ReadAllBytes(file.FullName)
                    };
                    break;
                }
            }
            Console.WriteLine("exit get avatar file");
            return result;
        }

        public bool IsUserHasAvatar(string username)
        {
            Console.WriteLine("Call IsUserHasAvatar service. params: username = " + username);
            bool res = false;
            FileInfo file;
            for (int i = 0; i < SUPPORT_AVATAR_EXTENSIONS.Length; i++)
            {
                file = new FileInfo(AVATAR_LOCATION + username + "." + SUPPORT_AVATAR_EXTENSIONS[i]);
                if (file.Exists)
                {
                    res = true;
                    break;
                }
            }
            Console.WriteLine("exit IsUserHasAvatar services");
            return res;
        }

        public List<User> FindStranger(string curUsername, string strangerUsername)
        {
            Console.WriteLine("call FindStranger service; params: curUsername = " + curUsername + "; strangerUsername = " + strangerUsername);
            List<User> result = new List<User>();
            if (strangerUsername.Length > 0)
            {
                lock (_synObj)
                {
                    User curUser = _allUsers.Find(x => x.Username == curUsername);
                    foreach (User user in _allUsers)
                    {
                        if (!user.Username.Contains(strangerUsername) || 
                            user == curUser) continue;
                        bool isStranger = true;
                        foreach (NodeRelationship node in _relationship[curUser].Friends)
                        {
                            if (node.User == user)
                            {
                                isStranger = false;
                                break;
                            }
                        }
                        foreach (NodeRelationship node in _relationship[curUser].ReceivedPendingRequest)
                        {
                            if (node.User == user || !isStranger)
                            {
                                isStranger = false;
                                break;
                            }
                        }
                        foreach (NodeRelationship node in _relationship[curUser].SentPendingRequest)
                        {
                            if (node.User == user || !isStranger)
                            {
                                isStranger = false;
                                break;
                            }
                        }
                        if (isStranger)
                        {
                            result.Add(user);
                        }
                    }
                }
            }
            Console.WriteLine("exit FindStranger service");
            return result;
        }

        public void SendMessage(DataMessage message)
        {
            Console.WriteLine("Call SendMessage");
            int id = AddMessageIntoDatabase(message);
            message.Id = id;
            message.Sender = _allUsers.Find(x => x.Username == message.Sender.Username);
            message.Receiver = _allUsers.Find(x => x.Username == message.Receiver.Username);
            Console.WriteLine("sender: " + message.Sender.Username + "; to: " + message.Receiver.Username + "; message: " + message.Message);
            lock (_synNotReceivedMessage)
            {
                _notReceivedMessages.Add(message);
            }

            if (_onlineUsers.ContainsKey(message.Receiver))
            {
                bool isReceived = false;
                try
                {
                    isReceived = _onlineUsers[message.Receiver].ReceiveMessage(message);
                }
                catch
                {
                    Console.WriteLine("Cant find user online to send message");
                    _onlineUsers.Remove(message.Receiver);
                }
                if (isReceived)
                {
                    lock (_synNotReceivedMessage)
                    {
                        UpdateReceivedMessageIntoDatabase(message);
                        _notReceivedMessages.Remove(message);
                    }
                }
            }
            else
            {
                //cur User is offline
            }
            Console.WriteLine("exit SendMessage service");
        }

        public List<DataMessage> GetUnreceivedMessages(string username)
        {
            Console.WriteLine("Call getUnreceivedMessages. params: username = " + username);
            List<DataMessage> UnreceivedMessages;
            User curUser = _allUsers.Find(x => x.Username == username);
            lock (_synNotReceivedMessage)
            {
                UnreceivedMessages = _notReceivedMessages.FindAll(x => x.Receiver == curUser);
                foreach (DataMessage message in UnreceivedMessages)
                {
                    UpdateReceivedMessageIntoDatabase(message);
                    _notReceivedMessages.Remove(message);
                }
            }
            Console.WriteLine("exit GetUnreceivedMessages");
            return UnreceivedMessages;
        }

        public bool UpdateInformation(User user)
        {
            Console.WriteLine("Call UpdateInformation");
            bool res = false;
            User curUser;
            lock (_synObj)
            {
                curUser = _allUsers.Find(x => x.Username == user.Username);
                curUser.Name = user.Name;
                curUser.IsMale = user.IsMale;
                res = UpdateUserInformation(curUser);
                if (res)
                {
                    foreach (NodeRelationship node in _relationship[curUser].Friends)
                    {
                        if (_onlineUsers.ContainsKey(node.User))
                        {
                            _onlineUsers[node.User].FriendOnlineListChangeUnexpectedly();
                        }
                    }
                }
            }
            Console.WriteLine("Exit UpdateInformation");
            return res;
        }

        public bool UpdatePassword(string username, string oldPass, string newPass)
        {
            Console.WriteLine("Call updatePassword");
            bool res = false;
            string query = "UPDATE users " +
                "SET password = @pass " +
                "WHERE username = @username and password = @oldPassword";
            using (SqlConnection connection = new SqlConnection(_strConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@pass", newPass);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@oldPassword", oldPass);

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                res = command.ExecuteNonQuery() == 1;
            }
            Console.WriteLine("Exit Update Password");
            return res;
        }

        public bool UpdateAvatar(string username, DataFile newAvatar)
        {
            Console.WriteLine("Call updateAvatar");
            bool res = false;
            int wildcardPos = newAvatar.FileName.LastIndexOf('.');
            string filename = username + newAvatar.FileName.Substring(wildcardPos);

            FileInfo file;
            for (int i = 0; i < SUPPORT_AVATAR_EXTENSIONS.Length; i++)
            {
                file = new FileInfo(AVATAR_LOCATION + username + "." + SUPPORT_AVATAR_EXTENSIONS[i]);
                if (file.Exists)
                {
                    file.Delete();
                }
            }

            File.WriteAllBytes(AVATAR_LOCATION + filename, newAvatar.Data);
            res = true;

            lock (_synObj)
            {
                User curUser = _allUsers.Find(x => x.Username == username);
                foreach (NodeRelationship node in _relationship[curUser].Friends)
                {
                    if (_onlineUsers.ContainsKey(node.User))
                        _onlineUsers[node.User].FriendChangeAvatar(curUser);
                }
            }
            Console.WriteLine("exit update avatar");
            return res;
        }

        public void Writting(User writer, User waiter)
        {
            Console.WriteLine("Call Writting");
            lock (_synObj)
            {
                if (_onlineUsers.ContainsKey(waiter))
                {
                    _onlineUsers[waiter].FriendIsWrittingMessage(writer);
                }
            }
            Console.WriteLine("Exit Writting");
        }

        public void StopWritting(User writer, User friend)
        {
            Console.WriteLine("Call Stop Writting");
            lock (_synObj)
            {
                if (_onlineUsers.ContainsKey(friend))
                {
                    _onlineUsers[friend].FriendStopWrittingMessage(writer);
                }
            }
            Console.WriteLine("Exit Stop Writting");
        }

        public void SendFriendRequest(string sender, string stranger)
        {
            Console.WriteLine("call SendFriendRequest");
            lock (_synObj)
            {
                User senderUser = _allUsers.Find(x => x.Username == sender);
                User strangerUser = _allUsers.Find(x => x.Username == stranger);
                MakePendingRequest(senderUser, strangerUser);
                InsertPendingRequestIntoDatabase(sender, stranger);
                if (_onlineUsers.ContainsKey(strangerUser))
                    _onlineUsers[strangerUser].ReceiveMakeFriendRequest(senderUser);
                if (_onlineUsers.ContainsKey(senderUser))
                    _onlineUsers[senderUser].SentMakeFriendRequest(strangerUser);
            }
            Console.WriteLine("Exit SendFriendRequest");
        }

        public List<User> GetPendingFriendRequests(User user)
        {
            Console.WriteLine("Call getpendingFriendRequests");
            List<User> res = new List<User>();
            lock (_synObj)
            {
                foreach (NodeRelationship node in _relationship[user].ReceivedPendingRequest)
                {
                    res.Add(node.User);
                }
            }
            Console.WriteLine("exit getPendingFriendRequests");
            return res;
        }

        public void AcceptFriendRequest(User foo, User bar)
        {
            Console.WriteLine("call AcceptFriendRequest");
            foo = _allUsers.Find(x => x.Username == foo.Username);
            bar = _allUsers.Find(x => x.Username == bar.Username);
            lock (_synObj)
            {
                _relationship[foo].SentPendingRequest.Remove(_relationship[bar]);
                _relationship[bar].ReceivedPendingRequest.Remove(_relationship[foo]);
                _relationship[foo].Friends.Add(_relationship[bar]);
                _relationship[bar].Friends.Add(_relationship[foo]);
                UpdateFriendshipIntoDatabase(foo.Username, bar.Username);
            }
            if (_onlineUsers.ContainsKey(foo))
                _onlineUsers[foo].GotANewFriend(bar);
            if (_onlineUsers.ContainsKey(bar))
                _onlineUsers[bar].GotANewFriend(foo);
            Console.WriteLine("exit AcceptFriendRequest");
        }

        public List<User> GetSentFriendRequest(User user)
        {
            Console.WriteLine("call getSEntFriendRequest");
            List<User> res = new List<User>();
            lock (_synObj)
            {
                foreach (NodeRelationship node in _relationship[user].SentPendingRequest)
                {
                    res.Add(node.User);
                }
            }
            Console.WriteLine("exit getSentFriendRequest");
            return res;
        }

        public User GetUserInformation(string username)
        {
            Console.WriteLine("Call GetUserInformation");
            User res = null;
            string query = "SELECT * FROM Users WHERE username = @username";
            using (SqlConnection connection = new SqlConnection(_strConnection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    res = new User()
                    {
                        Name = (string)reader["name"],
                        Username = (string)reader["username"],
                        IsMale = reader["gender"] as bool?
                    };
                }
            }
            Console.WriteLine("Exit GetUserInformation");
            return res;
        }

        public void Logout(User user)
        {
            Console.WriteLine("Call logout");
            RemoveUserOnline(user.Username);
            Console.WriteLine("Exit logout");
        }



        #endregion
    }
}
