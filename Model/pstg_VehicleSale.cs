using System;
using System.Collections.Generic;
using System.Text;

namespace APITriggerFunction.Model
{
    public class pstg_VehicleSale
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public int sales_id { get; set; }
        public int dealer_code { get; set; }
        public string branch_code { get; set; }
        public string model { get; set; }
        public string vin { get; set; }
        public int channel { get; set; }
        public int business_type { get; set; }
        public int segment { get; set; }
        public string version { get; set; }
        public string color { get; set; }
        public int type { get; set; }
        public int invoice_date { get; set; }
        public string branch_name { get; set; }
        public int price { get; set; }
        public string document_type { get; set; }
        public int document_number { get; set; }
        public string customer_id { get; set; }
        public string customer_name { get; set; }
        public string customer_address { get; set; }
        public string customer_city { get; set; }
        public string customer_phone { get; set; }
        public string customer_mobile { get; set; }
        public string customer_email { get; set; }
        public string vendor_id { get; set; }
        public string vendor_name { get; set; }
        public int nv_reference { get; set; }
        public string invoice_id { get; set; }
        public string customer_name_invoice { get; set; }
        public bool send_status { get; set; }
        public DateTime send_date { get; set; }
        public string fleet_id { get; set; }
        public DateTime? deliver_date { get; set; }
        public DateTime? reservation_date { get; set; }
        public string payment_method { get; set; }
        public string bank_name { get; set; }
        public string financing_term { get; set; }
        public string country_code { get; set; }
        public int channel_sender { get; set; }
    }
}
