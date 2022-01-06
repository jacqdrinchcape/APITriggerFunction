using APITriggerFunction.Model;
using APITriggerFunction.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            ResponseDetails resp = new ResponseDetails();

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var request = JsonConvert.DeserializeObject<List<pstg_SparePartStock>>(requestBody);

                var listSparePartStock = await _service.CreateListSparePartStockAsync(request);

                if (listSparePartStock.Count <= 0)
                {
                    resp.statuscode = errorcode.error;
                    resp.error_details.Add("error SparePartStockAPIFunc");
                    return new BadRequestObjectResult(resp);
                }
                else
                {
                    return new OkObjectResult(listSparePartStock);
                }

            }
            catch (Exception e)
            {
                log.LogError(e.ToString());

                resp.statuscode = errorcode.error;
                resp.error_details.Add(e.Message != null ? e.Message.ToString() : "error");
                return new BadRequestObjectResult(resp);
            }
        }
    }
}
