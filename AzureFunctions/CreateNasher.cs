using System.Collections.Generic;
using System.Net;
using AzureFunWithDapper.Entity;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunWithDapper.AzureFunctions
{
    public class CreateNasher
    {
        private readonly ILogger _logger;

        public CreateNasher(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CreateNasher>();
        }

        [Function("CreateNasher")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req, FunctionContext context)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            //NasherDetails myObject = (NasherDetails)context.Items["NasherID"];
            var t = req.FunctionContext.BindingContext.BindingData;
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
