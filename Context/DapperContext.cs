using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace AzureFunWithDapper.Context
{
    public class DapperContext
    {
        private readonly string _connectionString;
        public DapperContext()
        {
            _connectionString = Environment.GetEnvironmentVariable("SqlConnection");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
