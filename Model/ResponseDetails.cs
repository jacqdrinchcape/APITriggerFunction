using System;
using System.Collections.Generic;
using System.Text;

namespace APITriggerFunction.Model
{
    public class ResponseDetails
    {
        public errorcode statuscode { get; set; }
        public List<string> error_details { get; set; }
        
    }

    public enum errorcode
    {
        success = 0,
        error = 1,
        validation = 2,
        duplicate = 3,
        invalidvin = 4

    }

    public enum DocumentType
    {
        VehicleSales = 1,
        WorkOrders = 2,
        SparePartSales = 3,
        WorkOrderSpareParts = 4,
        SparePartStock = 5
    }
}
