using System.Data.SqlClient;

namespace SingleSignOn.Core.Services
{
    public interface IDatabaseConnection
    {
        string GetConnectionString();
        SqlConnection InitSqlConnection();
    }

    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        public SqlConnection InitSqlConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}