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
    public class WorkOrderSparePartsAPIFunc
    {
        private ICRUDService _service;
        public WorkOrderSparePartsAPIFunc(ICRUDService service)
        {
            _service = service;
        }

        [FunctionName("WorkOrderSparePartsAPIFunc")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            ResponseDetails resp = new ResponseDetails();

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var request = JsonConvert.DeserializeObject<List<pstg_WorkOrderSparePart>>(requestBody);

                var listWorkOrderSparePart = await _service.CreateListWorkOrderSparePartAsync(request);

                if (listWorkOrderSparePart.Count <= 0)
                {
                    resp.statuscode = errorcode.error;
                    resp.error_details.Add("error WorkOrderSparePartsAPIFunc");
                    return new BadRequestObjectResult(resp); 
                }
                else
                {
                    return new OkObjectResult(listWorkOrderSparePart);
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
