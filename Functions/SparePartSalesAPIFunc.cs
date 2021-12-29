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
using System.Collections.Generic;
using APITriggerFunction.Model;

namespace APITriggerFunction.Functions
{
    public class SparePartSalesAPIFunc
    {
        private ICRUDService _service;
        public SparePartSalesAPIFunc(ICRUDService service)
        {
            _service = service;
        }

        [FunctionName("SparePartSalesAPIFunc")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var request = JsonConvert.DeserializeObject<List<pstg_SparePartSale>>(requestBody);

                var listSparePartSale = await _service.CreateListSparePartSaleAsync(request);

                if (listSparePartSale.Count <= 0)
                {
                    return new BadRequestObjectResult($"error SparePartSalesAPIFunc");
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
