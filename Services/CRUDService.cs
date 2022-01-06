using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APITriggerFunction;
using APITriggerFunction.Model;
using System.ComponentModel.DataAnnotations;

namespace APITriggerFunction.Services
{
    public class CRUDService : ICRUDService
    {
        private readonly DataContext _ctx;
        public CRUDService(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<pstg_VehicleSaleResp>> CreateListVehicleSaleAsync(List<pstg_VehicleSale> vehiclesales)
        {
            List<pstg_VehicleSaleResp> lstVehicleSaleResp = new List<pstg_VehicleSaleResp>();


            foreach (var item in vehiclesales)
            {
                //Validation 
                var results = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(item, new ValidationContext(item, null, null), results, true);

                IEnumerable<ValidationResult> ValidationResults = results;

                //Initialize Response
                pstg_VehicleSaleResp resp = new pstg_VehicleSaleResp();
                resp.ResponseDetails = new ResponseDetails();
                resp.ResponseDetails.error_details = new List<string>();

                resp.pstg_VehicleSale = item;

                resp.pstg_VehicleSale.send_date = DateTime.Now;
                resp.pstg_VehicleSale.send_status = "0";
                resp.pstg_VehicleSale.channel_sender = 2;
 
				if (isValid)
				{
					try
					{
						//Check if VIN number exists in Master Table
						if (await _ctx.VehicleIdentificationNumbers.FirstOrDefaultAsync(x => x.vin == item.vin) == null)
						{
							//invalid vin
							resp.ResponseDetails.statuscode = errorcode.invalidvin;
							resp.ResponseDetails.error_details.Add(Helper.Helper.ErrorMessage((int)errorcode.invalidvin));
							lstVehicleSaleResp.Add(resp);
						}
						else
						{ 
							if (await _ctx.pstg_VehicleSales.FirstOrDefaultAsync(x => x.vin == item.vin) == null)
							{

								item.send_date = DateTime.Now;
								item.send_status = "1";
								item.channel_sender = 2;

								_ctx.pstg_VehicleSales.Add(item);

								var success = await _ctx.SaveChangesAsync() > 0;

								if (success)
								{
									resp.pstg_VehicleSale.send_status = "1";

                                    resp.ResponseDetails.statuscode = errorcode.success;
									resp.ResponseDetails.error_details.Add(Helper.Helper.ErrorMessage((int)errorcode.success));
									
								}
								else
								{
									resp.ResponseDetails.statuscode = errorcode.error;
									resp.ResponseDetails.error_details.Add(Helper.Helper.ErrorMessage((int)errorcode.error));
									
								}
							}
							else
							{
								//Save Error Logs
								pstg_ErrorLog err = new pstg_ErrorLog();
								err.vehicle_brand = item.vin;
								err.document_type = 1;
								err.error_message = "Duplicate record...";
								//Create error logs in DB
								await CreateErrorLogAsync(err);

								//set response details 
								resp.ResponseDetails.statuscode = errorcode.duplicate;
								resp.ResponseDetails.error_details.Add(Helper.Helper.ErrorMessage((int)errorcode.duplicate));
						
							}
						}
					}
					catch (Exception e)
					{
						//Save Error Logs
						pstg_ErrorLog err = new pstg_ErrorLog();
						err.vin = item.vin;
						err.document_type = 1;
						err.error_message = e.Message != null ? e.Message.ToString() : Helper.Helper.ErrorMessage((int)errorcode.error);
						//Create error logs in DB
						await CreateErrorLogAsync(err);

						//set response details 
						resp.ResponseDetails.statuscode = errorcode.error;
						resp.ResponseDetails.error_details.Add(e.Message != null ? e.Message.ToString() : Helper.Helper.ErrorMessage((int)errorcode.error));
					}
				}
				//validation
				else
				{

					resp.ResponseDetails.statuscode = errorcode.validation;
					results.ForEach(x => resp.ResponseDetails.error_details.Add(x.ErrorMessage.ToString()));
				}

                lstVehicleSaleResp.Add(resp);
            }
            return lstVehicleSaleResp;
        }

        public async Task<List<pstg_WorkOrderResp>> CreateListWorkOrderAsync(List<pstg_WorkOrder> workorders)
        {
            List<pstg_WorkOrderResp> lstWorkOrderResp = new List<pstg_WorkOrderResp>();


            foreach (var item in workorders)
            {
                //Validation 
                var results = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(item, new ValidationContext(item, null, null), results, true);

                IEnumerable<ValidationResult> ValidationResults = results;


                //Initialize Response
                pstg_WorkOrderResp resp = new pstg_WorkOrderResp();
                resp.ResponseDetails = new ResponseDetails();
                resp.ResponseDetails.error_details = new List<string>();

                resp.pstg_WorkOrder = item;

                resp.pstg_WorkOrder.send_date = DateTime.Now;
                resp.pstg_WorkOrder.send_status = "0";
                resp.pstg_WorkOrder.channel_sender = 2;

                if (isValid)
                {
                    try
                    {
                        if (await _ctx.pstg_WorkOrders.FirstOrDefaultAsync(x => x.dealer_code == item.dealer_code
                                                                             && x.joborder_number == item.joborder_number) == null)
                        {
                            item.send_date = DateTime.Now;
                            item.send_status = "1";
                            item.channel_sender = 2;

                            _ctx.pstg_WorkOrders.Add(item);

                            var success =  await _ctx.SaveChangesAsync() > 0;

                            if (success)
                            {
                                resp.pstg_WorkOrder.send_status = "1";

                                resp.ResponseDetails.statuscode = errorcode.success;
                                resp.ResponseDetails.error_details.Add(Helper.Helper.ErrorMessage((int)errorcode.success));
                               
                            }
                            else
                            {
                                resp.ResponseDetails.statuscode = errorcode.error;
                                resp.ResponseDetails.error_details.Add(Helper.Helper.ErrorMessage((int)errorcode.error));
                                
                            }

                        }
                        else
                        {
                            //Save Error Logs
                            pstg_ErrorLog err = new pstg_ErrorLog();
                            err.dealer_code = item.dealer_code;
                            err.joborder_number = item.joborder_number;
                            err.document_type = (int)DocumentType.WorkOrders;
                            err.error_message = "Duplicate record...";
                            //Create error logs in DB
                            await CreateErrorLogAsync(err);

                            //set response details 
                            resp.ResponseDetails.statuscode = errorcode.duplicate;
                            resp.ResponseDetails.error_details.Add(Helper.Helper.ErrorMessage((int)errorcode.duplicate));
                            
                        }
                    }
                    catch (Exception e)
                    {  
                        //Save Error Logs
                        pstg_ErrorLog err = new pstg_ErrorLog();
                        err.dealer_code = item.dealer_code;
                        err.joborder_number = item.joborder_number;
                        err.document_type = (int)DocumentType.WorkOrders;
                        err.error_message = e.Message != null ? e.Message.ToString() : Helper.Helper.ErrorMessage((int)errorcode.error);
                        //Create error logs in DB
                        await CreateErrorLogAsync(err);

                        //set response details 
                        resp.ResponseDetails.statuscode = errorcode.error;
                        resp.ResponseDetails.error_details.Add(e.Message != null ? e.Message.ToString() : Helper.Helper.ErrorMessage((int)errorcode.error));
                       
                    }
                }
                else
                {
                    resp.ResponseDetails.statuscode = errorcode.validation;
                    results.ForEach(x => resp.ResponseDetails.error_details.Add(x.ErrorMessage.ToString()));

                }

                lstWorkOrderResp.Add(resp);
            }
           
            return lstWorkOrderResp;
        }


        public async Task<List<pstg_WorkOrderSparePartResp>> CreateListWorkOrderSparePartAsync(List<pstg_WorkOrderSparePart> workorderspareparts)
        {
            List<pstg_WorkOrderSparePartResp> lstWorkOrderSparePartResp = new List<pstg_WorkOrderSparePartResp>();


            foreach (var item in workorderspareparts)
            {
                //Validation 
                var results = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(item, new ValidationContext(item, null, null), results, true);

                IEnumerable<ValidationResult> ValidationResults = results;

                //Initialize Response
                pstg_WorkOrderSparePartResp resp = new pstg_WorkOrderSparePartResp();
                resp.ResponseDetails = new ResponseDetails();
                resp.ResponseDetails.error_details = new List<string>();

                resp.pstg_WorkOrderSparePart = item;

                resp.pstg_WorkOrderSparePart.send_date = DateTime.Now;
                resp.pstg_WorkOrderSparePart.send_status = "0";
                resp.pstg_WorkOrderSparePart.channel_sender = 2;


                if (isValid)
                {
                    try
                    {
                        if (await _ctx.pstg_WorkOrderSpareParts.FirstOrDefaultAsync(x => x.dealer_code == item.dealer_code
                                                                                && x.branch_code == item.branch_code
                                                                                && x.joborder_number == item.joborder_number
                                                                                && x.part_number == item.part_number) == null)
                        {
                            item.send_date = DateTime.Now;
                            item.send_status = "1";
                            item.channel_sender = 2;

                            _ctx.pstg_WorkOrderSpareParts.Add(item);
                            var success = await _ctx.SaveChangesAsync() > 0;

                            if (success)
                            {
                                resp.pstg_WorkOrderSparePart.send_status = "1";

                                resp.ResponseDetails.statuscode = errorcode.success;
                                resp.ResponseDetails.error_details.Add(Helper.Helper.ErrorMessage((int)errorcode.success));
                            }
                            else
                            {
                                resp.ResponseDetails.statuscode = errorcode.error;
                                resp.ResponseDetails.error_details.Add(Helper.Helper.ErrorMessage((int)errorcode.error));
                            }
                        }
                        else
                        {
                            //Save Error Logs
                            pstg_ErrorLog err = new pstg_ErrorLog();
                            err.dealer_code = item.dealer_code;
                            err.branch_code = item.branch_code;
                            err.joborder_number = item.joborder_number;
                            err.part_number = item.part_number;

                            err.document_type = (int)DocumentType.WorkOrderSpareParts;
                            err.error_message = "Duplicate record...";
                            //Create error logs in DB
                            await CreateErrorLogAsync(err);

                            //set response details 
                            resp.ResponseDetails.statuscode = errorcode.duplicate;
                            resp.ResponseDetails.error_details.Add(Helper.Helper.ErrorMessage((int)errorcode.duplicate));

                        }
                    }
                    catch (Exception e)
                    {
                        //Save Error Logs
                        pstg_ErrorLog err = new pstg_ErrorLog();
                        err.dealer_code = item.dealer_code;
                        err.branch_code = item.branch_code;
                        err.joborder_number = item.joborder_number;
                        err.part_number = item.part_number;

                        err.document_type = (int)DocumentType.WorkOrderSpareParts;
                        err.error_message = e.Message != null ? e.Message.ToString() : Helper.Helper.ErrorMessage((int)errorcode.error);
                        //Create error logs in DB
                        await CreateErrorLogAsync(err);

                        //set response details 
                        resp.ResponseDetails.statuscode = errorcode.error;
                        resp.ResponseDetails.error_details.Add(e.Message != null ? e.Message.ToString() : Helper.Helper.ErrorMessage((int)errorcode.error));

                    }
                }
                //validation
                else
                {
                    resp.ResponseDetails.statuscode = errorcode.validation;
                    results.ForEach(x => resp.ResponseDetails.error_details.Add(x.ErrorMessage.ToString()));
                }

                lstWorkOrderSparePartResp.Add(resp);

            }
            return lstWorkOrderSparePartResp;
        }


        public async Task<List<pstg_SparePartSaleResp>> CreateListSparePartSaleAsync(List<pstg_SparePartSale> sparepartsales)
        {
            List<pstg_SparePartSaleResp> lstSparePartSaleResp = new List<pstg_SparePartSaleResp>();


            foreach (var item in sparepartsales)
            {
                //Validation 
                var results = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(item, new ValidationContext(item, null, null), results, true);

                IEnumerable<ValidationResult> ValidationResults = results;

                //Initialize Response
                pstg_SparePartSaleResp resp = new pstg_SparePartSaleResp();
                resp.ResponseDetails = new ResponseDetails();
                resp.ResponseDetails.error_details = new List<string>();

                resp.pstg_SparePartSale = item;

                resp.pstg_SparePartSale.send_date = DateTime.Now;
                resp.pstg_SparePartSale.send_status = "0";
                resp.pstg_SparePartSale.channel_sender = 2;

                if (isValid)
                {
                    try
                    {
                        if (await _ctx.pstg_SparePartSales.FirstOrDefaultAsync(x => x.dealer_code == item.dealer_code
                                                                                   && x.branch_office == item.branch_office
                                                                                   && x.part_number == item.part_number) == null)
                        {

                            item.send_date = DateTime.Now;
                            item.send_status = "1";
                            item.channel_sender = 2;

                            _ctx.pstg_SparePartSales.Add(item);
                            var success =  await _ctx.SaveChangesAsync() > 0;

                            if (success)
                            {
                                resp.pstg_SparePartSale.send_status = "1";

                                resp.ResponseDetails.statuscode = errorcode.success;
                                resp.ResponseDetails.error_details.Add(Helper.Helper.ErrorMessage((int)errorcode.success));

                            }
                            else
                            {
                                resp.ResponseDetails.statuscode = errorcode.error;
                                resp.ResponseDetails.error_details.Add(Helper.Helper.ErrorMessage((int)errorcode.error));

                            }
                        }
                        else
                        {
                            //Save Error Logs
                            pstg_ErrorLog err = new pstg_ErrorLog();
                            err.dealer_code = item.dealer_code;
                            err.branch_office = item.branch_office;
                            err.part_number = item.part_number;
                            err.document_type = (int)DocumentType.SparePartSales;
                            err.error_message = "Duplicate record...";
                            //Create error logs in DB
                            await CreateErrorLogAsync(err);

                            //set response details 
                            resp.ResponseDetails.statuscode = errorcode.duplicate;
                            resp.ResponseDetails.error_details.Add(Helper.Helper.ErrorMessage((int)errorcode.duplicate));

                        }
                    }
                    catch (Exception e)
                    {

                        //Save Error Logs
                        pstg_ErrorLog err = new pstg_ErrorLog();
                        err.dealer_code = item.dealer_code;
                        err.branch_office = item.branch_office;
                        err.part_number = item.part_number;
                        err.document_type = (int)DocumentType.SparePartSales;
                        err.error_message = e.Message != null ? e.Message.ToString() : Helper.Helper.ErrorMessage((int)errorcode.error);
                        //Create error logs in DB
                        await CreateErrorLogAsync(err);

                        //set response details 
                        resp.ResponseDetails.statuscode = errorcode.error;
                        resp.ResponseDetails.error_details.Add(e.Message != null ? e.Message.ToString() : Helper.Helper.ErrorMessage((int)errorcode.error));

                    }
                }
                else
                {
                    resp.ResponseDetails.statuscode = errorcode.validation;
                    results.ForEach(x => resp.ResponseDetails.error_details.Add(x.ErrorMessage.ToString()));

                }

                lstSparePartSaleResp.Add(resp);
            }
            

            return lstSparePartSaleResp;
        }


        public async Task<List<pstg_SparePartStockResp>> CreateListSparePartStockAsync(List<pstg_SparePartStock> sparepartstocks)
        {
            List<pstg_SparePartStockResp> lstSparePartStockResp = new List<pstg_SparePartStockResp>();


            foreach (var item in sparepartstocks)
            {
                //Validation 
                var results = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(item, new ValidationContext(item, null, null), results, true);

                IEnumerable<ValidationResult> ValidationResults = results;


                //Initialize Response
                pstg_SparePartStockResp resp = new pstg_SparePartStockResp();
                resp.ResponseDetails = new ResponseDetails();
                resp.ResponseDetails.error_details = new List<string>();

                resp.pstg_SparePartStock = item;

                resp.pstg_SparePartStock.send_date = DateTime.Now;
                resp.pstg_SparePartStock.send_status = "0";
                resp.pstg_SparePartStock.channel_sender = 2;

                if (isValid)
                { 
                    try
                    {
                        if (await _ctx.pstg_SparePartStocks.FirstOrDefaultAsync(x => x.vehicle_brand == item.vehicle_brand 
                                                                                    && x.branch_office == item.branch_office 
                                                                                    && x.part_number == item.part_number) == null)
                        {
                            item.send_date = DateTime.Now;
                            item.send_status = "1";
                            item.channel_sender = 2;

                            _ctx.pstg_SparePartStocks.Add(item);
                        
                            var success = await _ctx.SaveChangesAsync() > 0;

                            if (success)
                            {
                                resp.pstg_SparePartStock.send_status = "1";

                                resp.ResponseDetails.statuscode = errorcode.success;
                                resp.ResponseDetails.error_details.Add(Helper.Helper.ErrorMessage((int)errorcode.success));
                               
                            }
                            else
                            {
                                resp.ResponseDetails.statuscode = errorcode.error;
                                resp.ResponseDetails.error_details.Add(Helper.Helper.ErrorMessage((int)errorcode.error));
                                
                            }
                        }
                        else
                        {
                            //Save Error Logs
                            pstg_ErrorLog err = new pstg_ErrorLog();
                            err.vehicle_brand = item.vehicle_brand;
                            err.branch_office = item.branch_office;
                            err.part_number = item.part_number;
                            err.document_type = 5;
                            err.error_message = "Duplicate record...";
                            //Create error logs in DB
                            await CreateErrorLogAsync(err);

                            //set response details 
                            resp.ResponseDetails.statuscode = errorcode.duplicate;
                            resp.ResponseDetails.error_details.Add(Helper.Helper.ErrorMessage((int)errorcode.duplicate));
                           
                        }
                    }
                    catch (Exception e)
                    {
                        //Save Error Logs
                        pstg_ErrorLog err = new pstg_ErrorLog();
                        err.vehicle_brand = item.vehicle_brand;
                        err.branch_office = item.branch_office;
                        err.part_number = item.part_number;
                        err.document_type = 5;
                        err.error_message = e.Message != null ? e.Message.ToString() : Helper.Helper.ErrorMessage((int)errorcode.error);
                        //Create error logs in DB
                        await CreateErrorLogAsync(err);

                        //set response details 
                        resp.ResponseDetails.statuscode = errorcode.error;
                        resp.ResponseDetails.error_details.Add(e.Message != null ? e.Message.ToString() : Helper.Helper.ErrorMessage((int)errorcode.error));
                       
                    }
                }
                //validation
                else
                {

                    resp.ResponseDetails.statuscode = errorcode.validation;
                    results.ForEach(x=> resp.ResponseDetails.error_details.Add(x.ErrorMessage.ToString()));
                    
                }

                lstSparePartStockResp.Add(resp);
            }
         
            return lstSparePartStockResp;
        }

        public async Task<pstg_SparePartStock> CreateSparePartStockAsync(pstg_SparePartStock sparepartstock)
        {
            _ctx.pstg_SparePartStocks.Add(sparepartstock);
            await _ctx.SaveChangesAsync();
            return sparepartstock;
        }

        public async Task<pstg_ErrorLog> CreateErrorLogAsync(pstg_ErrorLog errorlog)
        {
            errorlog.send_date = DateTime.Now;
            errorlog.channel_sender = 2;
            _ctx.pstg_ErrorLogs.Add(errorlog);
            await _ctx.SaveChangesAsync();
            return errorlog;
        }
    }
}