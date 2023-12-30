using System.Collections.Generic;
using System.Data;
using System.Net;
using AzureFunWithDapper.Context;
using AzureFunWithDapper.Entity;
using AzureFunWithDapper.Utilities;
using Dapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunWithDapper.AzureFunctions
{
    public class CreateNasher
    {
        private readonly ILogger _logger;
        private readonly DapperContext _dapperContext;

        public CreateNasher(ILoggerFactory loggerFactory, DapperContext dapperContext)
        {
            _logger = loggerFactory.CreateLogger<CreateNasher>();
            _dapperContext= dapperContext;
        }

        [Function("CreateNasher")]
        public async Task<string> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req, FunctionContext context)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                var json = await ReqBodyDeserializer.Deserializer(req);
                var query = "INSERT INTO NasherDetails (FirstName, LastName, Department, Email, ContactNumber, GitURL, LinkedInURL, IsActive) VALUES (@FirstName, @LastName, @Department, @Email, @ContactNumber, @GitURL, @LinkedInURL, @IsActive)"+
                            "SELECT CAST(SCOPE_IDENTITY() as int)";
                var parameters = new DynamicParameters(); 
                parameters.Add("FirstName", json.FirstName, DbType.String);
                parameters.Add("LastName", json.LastName, DbType.String);
                parameters.Add("Department", json.Department, DbType.String);
                parameters.Add("Email", json.Email, DbType.String);
                parameters.Add("ContactNumber", json.ContactNumber, DbType.String);
                parameters.Add("GitURL", json.GitURL, DbType.String);
                parameters.Add("LinkedInURL", json.LinkedInURL, DbType.String);
                parameters.Add("IsActive", json.IsActive, DbType.Boolean);

                using (var connection = _dapperContext.CreateConnection())
                {
                    var createdNasher = await connection.QuerySingleAsync<int>(query, parameters);
                    return "User created with ID = " + createdNasher.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }
    }
}
