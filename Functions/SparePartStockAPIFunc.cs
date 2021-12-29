using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using APITriggerFunction.Services;
using APITriggerFunction.Model;
using System.Collections.Generic;

namespace APITriggerFunction.Functions
{
    public class SparePartStockAPIFunc
    {
        private ICRUDService _service;
        public SparePartStockAPIFunc(ICRUDService service)
        {
            _service = service;
        }

        [FunctionName("SparePartStockAPIFunc")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var request = JsonConvert.DeserializeObject<List<pstg_SparePartStock>>(requestBody);

                var listSparePartStock = await _service.CreateListSparePartStockAsync(request);

                if (listSparePartStock.Count <= 0)
                {
                    return new BadRequestObjectResult($"error SparePartStockAPIFunc");
                }

            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
                return new BadRequestResult();
            }
            return new OkResult();
        }
    }
}
