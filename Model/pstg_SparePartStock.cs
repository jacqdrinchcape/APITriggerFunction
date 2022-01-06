using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using static APITriggerFunction.Model.ValidationExtension;

namespace APITriggerFunction.Model
{
    public class pstg_SparePartStock
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string dealer_id { get; set; }
        public string vehicle_brand { get; set; }
        public string branch_office { get; set; }
        public string part_number { get; set; }
        public string description { get; set; }
        public string amount { get; set; }
        public string send_status { get; set; }
        public DateTime send_date { get; set; }
        public string country_code { get; set; }
        public int channel_sender { get; set; }
    }

}
