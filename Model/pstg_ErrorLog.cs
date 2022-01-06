using System;
using System.Collections.Generic;
using System.Text;

namespace APITriggerFunction.Model
{
    public class pstg_ErrorLog
    {
        public int id { get; set; }
        public int document_type { get; set; }
        public string vin { get; set; }
        public int dealer_code { get; set; }
        public int branch_code { get; set; }
        public string joborder_number { get; set; }
        public string vehicle_brand { get; set; }
        public string part_number { get; set; }
        public string branch_office { get; set; }
        public DateTime send_date { get; set; }
        public string error_message { get; set; }
        public int channel_sender { get; set; }
    }
}
