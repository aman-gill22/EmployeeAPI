using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Data;

namespace EmployeeAPI
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("gLearnDbConnectionString");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
