using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;
using System.Web;
using static APITriggerFunction.Model.ValidationExtension;

namespace APITriggerFunction.Model
{
	[AtLeastOneProperty("person_type", "business_name", "client_name", ErrorMessage = "You must supply at least one value - person_type, business_name, client_name")]
	[AtLeastOneProperty("business_phone", "home_phone", "mobile_phone", "contact_phone", ErrorMessage = "You must supply at least one value - business_phone, home_phone, mobile_phone, contact_phone")]
	[AtLeastOneProperty("maintenance", "warranty", "dent_painting_service", "repair", "accessories", "internal_joborder", ErrorMessage = "You must supply at least one value - maintenance, warranty, dent_painting_service, repair, accessories, internal_joborder")]
	[AtLeastOneProperty("amount_labor", "laborcost_painting", "amount_spare_parts", "amount_sp_plaza", "amount_sp_collision", "amount_lubricants", "amount_thirdparty", "amount_materials", "discounts", ErrorMessage = "You must supply at least one value - amount_labor, laborcost_painting, amount_spare_parts, amount_sp_plaza, amount_sp_collision, amount_lubricants, amount_thirdparty, amount_materials, discounts")]
	public class pstg_WorkOrder
    {
		public Guid id { get; set; } = Guid.NewGuid();
		[Required(ErrorMessage = "joborder_id required")]
		public int joborder_id { get; set; }
		[Required(ErrorMessage = "dealer_code required")]
		public int dealer_code { get; set; }
		[Required(ErrorMessage = "joborder_number required")]
		public string joborder_number { get; set; }
		[Required(ErrorMessage = "vehicle_brand required")]
		public int vehicle_brand { get; set; }
		[Required(ErrorMessage = "branch_office required")]
		public string branch_office { get; set; }
		public string workshop_id { get; set; }
		[Required(ErrorMessage = "start_date required")]
		public int start_date { get; set; }
		[Required(ErrorMessage = "reception_id required")]
		public string reception_id { get; set; }
		[Required(ErrorMessage = "reception_name required")]
		public string reception_name { get; set; }
		public string mechanic_id { get; set; }
		public string mechanic_name { get; set; }
		public string vehicle_insurance { get; set; }
		[Required(ErrorMessage = "vin required")]
		public string vin { get; set; }
		[Required(ErrorMessage = "license_plate required")]
		public string license_plate { get; set; }
		public string model { get; set; }
		public string motor_number { get; set; }
		public string chasis_number { get; set; }
		[Required(ErrorMessage = "kilometers required")]
		public string kilometers { get; set; }
		[Required(ErrorMessage = "client_id required")]
		public string client_id { get; set; }
		[Required(ErrorMessage = "person_type required")]
		public string person_type { get; set; }
		public string business_name { get; set; }
		public string client_name { get; set; }
		public string client_surname { get; set; }
		public string sex { get; set; }
		public int birth_date { get; set; }
		[Required(ErrorMessage = "customer_address required")]
		public string customer_address { get; set; }
		public string customer_city { get; set; }
		[Required(ErrorMessage = "zip_code required")]
		public int zip_code { get; set; }
		[Required(ErrorMessage = "region_name required")]
		public string region_name { get; set; }
		[Required(ErrorMessage = "community_name required")]
		public string community_name { get; set; }
		public string business_phone { get; set; }
		public string home_phone { get; set; }
		public string mobile_phone { get; set; }
		public string contact_phone { get; set; }
		[DataType(DataType.EmailAddress)]
		[RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
		public string owner_email_address { get; set; }
		public string contact_type { get; set; }
		public string contact_name { get; set; }
		public string contact_surname { get; set; }
		[DataType(DataType.EmailAddress)]
		[RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
		public string contact_email { get; set; }
		[Required(ErrorMessage = "joborder_status required")]
		public string joborder_status { get; set; }
		public string operation_code { get; set; }
		public string operation_name { get; set; }
		public string maintenance { get; set; }
		public string warranty { get; set; }
		public string dent_painting_service { get; set; }
		public string repair { get; set; }
		public string accessories { get; set; }
		public string internal_joborder { get; set; }
		public int amount_labor { get; set; }
		public int laborcost_painting { get; set; }
		public int amount_spare_parts { get; set; }
		public int amount_sp_plaza { get; set; }
		public int amount_sp_collision { get; set; }
		public int amount_lubricants { get; set; }
		public int amount_thirdparty { get; set; }
		public int amount_materials { get; set; }
		public int discounts { get; set; }
		public string total_workhours { get; set; }
		public int mileage_maintenance { get; set; }
		[Required(ErrorMessage = "delivered_date required")]
		public int delivered_date { get; set; }
		public int invoice_date { get; set; }
		[Required(ErrorMessage = "bill_number required")]
		public int bill_number { get; set; }
		[Required(ErrorMessage = "invoice_id required")]
		public string invoice_id { get; set; }
		[Required(ErrorMessage = "customer_name_invoice required")]
		public string customer_name_invoice { get; set; }
		[Required(ErrorMessage = "joborder_description required")]
		public string joborder_description { get; set; }
		public string insurance_name { get; set; }
		public int deductible { get; set; }
		public string send_status { get; set; }
		public DateTime send_date { get; set; }
		public string closing_date { get; set; }
		public string fleet_id { get; set; }
		public string reason_service { get; set; }
		public string country_code { get; set; }
		public int channel_sender { get; set; }
	}
}
