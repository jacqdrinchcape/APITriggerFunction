using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using APITriggerFunction.Model;
using Microsoft.Extensions.Logging;
using APITriggerFunction.Services;
using Microsoft.AspNetCore.Mvc;

namespace APITriggerFunction.Functions
{
    public class SparePartStockBlobFunc
    {
        private ICRUDService _service;
        public SparePartStockBlobFunc(ICRUDService service)
        {
            _service = service;
        }

        [FunctionName("SparePartStockBlobFunc")]
        public async Task Run([BlobTrigger("sparepart-stocks/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            if (myBlob.Length > 0)
            {
                using (var reader = new StreamReader(myBlob))
                {
                    var lineNumber = 1;
                    var line = await reader.ReadLineAsync();
                    while (line != null)
                    {
                        await ProcessLine(name, line, lineNumber, log);
                        line = await reader.ReadLineAsync();
                        lineNumber++;
                    }
                }
            }

        }


        private async Task<IActionResult> ProcessLine(string name, string line, int lineNumber, ILogger log)
        {

            try
            {
                log.LogInformation($"ProcessLine...");

                if (string.IsNullOrWhiteSpace(line))
                {
                    log.LogWarning($"{name}: {lineNumber} is empty.");
                    return new BadRequestObjectResult($"line is empty!");
                }

                var parts = line.Split(',');
                if (parts.Length != 7)
                {
                    log.LogError($"{name}: {lineNumber} invalid data: {line}.");
                    return new BadRequestObjectResult($"invalid data!");
                }

                var item = new pstg_SparePartStock
                {
                    dealer_id = parts[0],
                    vehicle_brand = parts[1],
                    branch_office = parts[2],
                    part_number = parts[3],
                    description = parts[4],
                    amount = parts[5],
                    send_status = "1",
                    send_date = DateTime.Now,
                    country_code = parts[6],
                    channel_sender = 1,
                };



                //if ((int.TryParse(parts[1], out int complete) == false) || complete < 0 || complete > 1)
                //{
                //    log.Error($"{name}: {lineNumber} bad complete flag: {parts[1]}.");
                //}
                //item.IsComplete = complete == 1;

                var user = await _service.CreateSparePartStockAsync(item);
                if (user == null)
                {
                    return new BadRequestObjectResult($"username exists!");
                }

                var responseMsg = $"SparePartStock is created";
                return new OkObjectResult(responseMsg);

            }
            catch (Exception e)
            {

                log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Error: {e.InnerException} Bytes");
                throw;
            }

        }
    }
}
