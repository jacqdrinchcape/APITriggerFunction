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
    public class VehicleSaleAPIFunc
    {
        private ICRUDService _service;
        public VehicleSaleAPIFunc(ICRUDService service)
        {
            _service = service;
        }

        [FunctionName("VehicleSaleAPIFunc")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var request = JsonConvert.DeserializeObject<List<pstg_VehicleSale>>(requestBody);

                var listSListVehicleSale = await _service.CreateListVehicleSaleAsync(request);

                if (listSListVehicleSale.Count <= 0)
                {
                    return new BadRequestObjectResult($"error VehicleSaleAPIFunc");
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
