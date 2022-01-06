using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static APITriggerFunction.Model.ValidationExtension;

namespace APITriggerFunction.Model
{
    public class pstg_SparePartSale
    {
		public Guid id { get; set; } = Guid.NewGuid();
		public int dealer_code { get; set; }
		public string branch_office { get; set; }
		public string workshop_id { get; set; }
		public int vehicle_brand { get; set; }
		public string part_number { get; set; }
		public string amount { get; set; }
		public string description { get; set; }
		public int net_price { get; set; }
		public int discount { get; set; }
		public int sales_price { get; set; }
		public string client_type { get; set; }
		public string document_type { get; set; }
		public int document_number { get; set; }
		public int document_date { get; set; }
		public int document_reference { get; set; }
		public string customer_id { get; set; }
		public string customer_name { get; set; }
		public string customer_address { get; set; }
		public string customer_telephone { get; set; }
		public string customer_mobile { get; set; }
		public string customer_city { get; set; }
		[Required(ErrorMessage = "vin required")]
		public string vin { get; set; }
		public string sales_channel { get; set; }
		public string send_status { get; set; }
		public DateTime send_date { get; set; }
		public string vendor_id { get; set; }
		public string license_plate { get; set; }
		public string payment_method { get; set; }
		public int channel_sender { get; set; }
		public string country_code { get; set; }
	}
}
