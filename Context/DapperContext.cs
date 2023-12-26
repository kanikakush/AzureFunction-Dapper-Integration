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
        //private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext()
        {
            //_configuration = configuration;
            _connectionString = Environment.GetEnvironmentVariable("SqlConnection");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
//1.GetNasherList(Http Trigger)

//    2.CreateNasher(Http Trigger)

//    3.GetNasherByID(Http Trigger)

//    4.UpdateNasheDetails(Http Trigger)

//    5.RemoveInActiveNasher(Timetrigger)
