using System;
using System.Collections.Generic;
using System.Text;

namespace APITriggerFunction.Model
{
    public class pstg_WorkOrder
    {
		public Guid id { get; set; } = Guid.NewGuid();
		public int joborder_id { get; set; }
		public int dealer_code { get; set; }
		public string joborder_number { get; set; }
		public int vehicule_brand { get; set; }
		public string branch_office { get; set; }
		public int start_date { get; set; }
		public string reception_id { get; set; }
		public string reception_name { get; set; }
		public string mechanic_id { get; set; }
		public string mechanic_name { get; set; }
		public string vehicule_insurance { get; set; }
		public string vin { get; set; }
		public string license_plate { get; set; }
		public string model { get; set; }
		public string motor_number { get; set; }
		public string chasis_number { get; set; }
		public string kilometers { get; set; }
		public string client_id { get; set; }
		public string person_type { get; set; }
		public string business_name { get; set; }
		public string client_name { get; set; }
		public string client_surname { get; set; }
		public string sex { get; set; }
		public int birth_date { get; set; }
		public string customer_address { get; set; }
		public string customer_city { get; set; }
		public int zip_code { get; set; }
		public string region_name { get; set; }
		public string community_name { get; set; }
		public string business_phone { get; set; }
		public string home_phone { get; set; }
		public string mobile_phone { get; set; }
		public string contact_phone { get; set; }
		public string owner_email_address { get; set; }
		public string contact_type { get; set; }
		public string contact_name { get; set; }
		public string contact_surname { get; set; }
		public string contact_email { get; set; }
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
		public int delivered_date { get; set; }
		public int invoice_date { get; set; }
		public int bill_number { get; set; }
		public string invoice_id { get; set; }
		public string customer_name_invoice { get; set; }
		public string joborder_description { get; set; }
		public string insurance_name { get; set; }
		public int deductible { get; set; }
		public bool send_status { get; set; }
		public DateTime send_date { get; set; }
		public string closing_date { get; set; }
		public string fleet_id { get; set; }
		public string reason_service { get; set; }
		public string country_code { get; set; }
		public int channel_sender { get; set; }
	}
}
