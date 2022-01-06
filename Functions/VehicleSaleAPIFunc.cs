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

            ResponseDetails resp = new ResponseDetails();

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var request = JsonConvert.DeserializeObject<List<pstg_VehicleSale>>(requestBody);

                var listSListVehicleSale = await _service.CreateListVehicleSaleAsync(request);

                if (listSListVehicleSale.Count <= 0)
                {
                    resp.statuscode = errorcode.error;
                    resp.error_details.Add("error VehicleSaleAPIFunc");
                    return new BadRequestObjectResult(resp);
                }
                else
                {
                    return new OkObjectResult(listSListVehicleSale);
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
