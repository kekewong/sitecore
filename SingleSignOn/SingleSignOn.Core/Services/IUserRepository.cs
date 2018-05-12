using System;
using System.Data;
using System.Data.SqlClient;
using NLog;
using SingleSignOn.Core.Domain;
using SingleSignOn.Core.DTOs;
using SingleSignOn.Core.Mediators.Messages;

namespace SingleSignOn.Core.Services
{
    public interface IUserRepository
    {
        User Get(string username, string password);
        void Create(CreateUserMessage message);
        bool UsernameExists(string username);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IUserQueryCacheService _userQueryCacheService;
        private readonly ISimplePasswordHash _simplePasswordHash;
        private readonly IDatabaseConnection _connection;
        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        public UserRepository(IDatabaseConnection connection, IUserQueryCacheService userQueryCacheService, ISimplePasswordHash simplePasswordHash)
        {
            _connection = connection;
            _userQueryCacheService = userQueryCacheService;
            _simplePasswordHash = simplePasswordHash;
        }

        public bool UsernameExists(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }

            var count = 0;
            var query = "SELECT Count(*) from dbo.users "
                        + "WHERE username = @username";

            using (var connection = _connection.InitSqlConnection())
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    try
                    {
                        connection.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            count = Convert.ToInt32(reader[0]);
                        }
                        reader.Close();

                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex);
                        throw;
                    }

                }
            }
            return count > 0;
        }

        public User Get(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }

            var passwordhash = _simplePasswordHash.Hash(password);

            var user = _userQueryCacheService.GetOrSet(username, () =>
             {
                 User searchedUser = null;
                 var query = "SELECT TOP 1 id,username,password,role from dbo.users "
                             + "WHERE username = @username and password = @password";

                 using (var connection = _connection.InitSqlConnection())
                 {
                     using (var command = new SqlCommand(query, connection))
                     {
                         command.Parameters.AddWithValue("@username", username);
                         command.Parameters.AddWithValue("@password", passwordhash);
                         try
                         {
                             connection.Open();
                             var reader = command.ExecuteReader();
                             while (reader.Read())
                             {
                                 searchedUser = new User
                                 {
                                     Id = Guid.Parse(reader[0].ToString()),
                                     Username = reader[1].ToString(),
                                     Password = reader[2].ToString(),
                                     Role = (UserRole)Enum.Parse(typeof(UserRole), reader[3].ToString())
                                 };
                             }
                             reader.Close();

                         }
                         catch (Exception ex)
                         {
                             _log.Error(ex);
                             throw;
                         }

                         return searchedUser;
                     }
                 }
             });
            return user;
        }

        public void Create(CreateUserMessage message)
        {
            string query = "INSERT INTO dbo.users (id,username, password, role) " +
                           "VALUES (@Id,@Username,@Password,@Role) ";

            using (var connection = _connection.InitSqlConnection())
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
                    command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = message.Username;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar, 4000).Value = message.Password;
                    command.Parameters.Add("@Role", SqlDbType.NVarChar, 50).Value = message.Role.ToString();

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex);
                        throw;
                    }
                }
            }

        }
    }
}
