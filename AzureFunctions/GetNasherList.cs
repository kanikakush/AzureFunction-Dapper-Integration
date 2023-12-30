using System.Collections.Generic;
using System.Net;
using AzureFunWithDapper.Context;
using AzureFunWithDapper.Entity;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunWithDapper
{
    public class GetNasherList
    {
        private readonly ILogger _logger;
        private readonly DapperContext _dapperContext;

        public GetNasherList(ILoggerFactory loggerFactory, DapperContext dapperContext)
        {
            _logger = loggerFactory.CreateLogger<GetNasherList>();
            _dapperContext = dapperContext;
        }

        [Function("GetNasherList")]
        public async Task<IList<NasherDetails>> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                var query = "SELECT NasherID, FirstName, LastName, Department, Email, ContactNumber, GitURL, LinkedInURL, IsActive  FROM NashTechDB.dbo.NasherDetails";
                using (var connection = _dapperContext.CreateConnection())
                {
                   var result = await connection.QueryAsync<NasherDetails>(query);
                   return result.ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
