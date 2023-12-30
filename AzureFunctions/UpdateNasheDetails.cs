using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
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
    public class UpdateNasheDetails
    {
        private readonly ILogger _logger;
        private readonly DapperContext _dapperContext;

        public UpdateNasheDetails(ILoggerFactory loggerFactory, DapperContext dapperContext)
        {
            _logger = loggerFactory.CreateLogger<UpdateNasheDetails>();
            _dapperContext = dapperContext;
        }

        [Function("UpdateNasheDetails")]
        public async Task<string> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                var json = await ReqBodyDeserializer.Deserializer(req);
                var query = "UPDATE NasherDetails SET IsActive = @IsActive, Department = @Department, ContactNumber = @ContactNumber WHERE NasherID = @NasherID;";
                var parameters = new DynamicParameters();
                parameters.Add("NasherID", json.NasherID, DbType.Int32);
                parameters.Add("Department", json.Department, DbType.String);
                parameters.Add("ContactNumber", json.ContactNumber, DbType.String);
                parameters.Add("IsActive", json.IsActive, DbType.Boolean);

                using (var connection = _dapperContext.CreateConnection())
                {
                    var p = await connection.ExecuteAsync(query, parameters);
                    return "Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
