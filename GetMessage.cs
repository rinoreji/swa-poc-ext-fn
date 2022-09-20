using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace swa_poc_ext_fn
{
    public static class GetMessage
    {
        [FunctionName("GetMessage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var headers = new List<string>();
            foreach (var key in req.Headers.Keys)
            {
                headers.Add($"{key}-{req.Headers[key]}");
            }
            
            return new JsonResult(new { requestBody, headers });
        }
    }
}
