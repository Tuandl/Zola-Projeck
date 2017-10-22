using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Configuration;
using System.Data.SqlClient;

namespace ServerLibrary
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,
        InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = false)]
    public class ChatService : IChatService
    {
        private string _strConnection;

        public ChatService()
        {
            _strConnection = getConnectionString();
        }

        #region private methods
        /// <summary>
        /// Get connection string from ConfigurationManager
        /// </summary>
        /// <returns>Connection string</returns>
        private string getConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["zola"].ConnectionString;
        }
        #endregion


        #region Implement IChatService
        public bool Login(string username, string password)
        {
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
                return reader.HasRows;
            }
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
        #endregion
    }
}
