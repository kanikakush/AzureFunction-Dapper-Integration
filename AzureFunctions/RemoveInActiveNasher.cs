using System;
using AzureFunWithDapper.Context;
using AzureFunWithDapper.Utilities;
using Dapper;
using System.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunWithDapper.AzureFunctions
{
    public class RemoveInActiveNasher
    {
        private readonly ILogger _logger;
        private readonly DapperContext _dapperContext;

        public RemoveInActiveNasher(ILoggerFactory loggerFactory, DapperContext dapperContext)
        {
            _logger = loggerFactory.CreateLogger<RemoveInActiveNasher>();
            _dapperContext = dapperContext;
        }

        [Function("RemoveInActiveNasher")]
        //0 */2 * * * * minute divide by 2
        //* * * * * * *
        public async Task<string> Run([TimerTrigger("0 35 16 * * *")] MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            try
            {
                var query = "DELETE FROM NasherDetails WHERE IsActive = 0";              
                using (var connection = _dapperContext.CreateConnection())
                {
                    var p = await connection.ExecuteAsync(query);
                    return "Inactive Nashers Deleted Successfully";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
