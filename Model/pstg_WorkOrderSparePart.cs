using System;
using System.Collections.Generic;
using System.Text;
using static APITriggerFunction.Model.ValidationExtension;

namespace APITriggerFunction.Model
{
    public class pstg_WorkOrderSparePart
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public int spare_id { get; set; }
        public int dealer_code { get; set; }
        public string workshop { get; set; }
        public int branch_code { get; set; }
        public string joborder_number { get; set; }
        public string part_number { get; set; }
        public string amount { get; set; }
        public int net_price { get; set; }
        public string description { get; set; }
        public string send_status { get; set; }
        public DateTime send_date { get; set; }
        public int channel_sender { get; set; }
        public string country_code { get; set; }
    }
}
