using AzureFunWithDapper.Entity;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunWithDapper.Utilities
{
    internal class ReqBodyDeserializer
    {
        public static async Task<NasherDetails> Deserializer(HttpRequestData req)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var json = JsonConvert.DeserializeObject<NasherDetails>(body);
            return json;
        }
    }
}
