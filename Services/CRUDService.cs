using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APITriggerFunction;
using APITriggerFunction.Model;

namespace APITriggerFunction.Services
{
    public class CRUDService : ICRUDService
    {
        private readonly DataContext _ctx;
        public CRUDService(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<pstg_VehicleSale>> CreateListVehicleSaleAsync(List<pstg_VehicleSale> vehiclesales)
        {
            try
            {
                foreach (var item in vehiclesales)
                {
                    item.send_date = DateTime.Now;
                    item.send_status = true;
                    item.channel_sender = 2;

                    _ctx.pstg_VehicleSales.Add(item);
                    await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return vehiclesales;
        }



        public async Task<List<pstg_WorkOrder>> CreateListWorkOrderAsync(List<pstg_WorkOrder> workorders)
        {
            try
            {
                foreach (var item in workorders)
                {
                    item.send_date = DateTime.Now;
                    item.send_status = true;
                    item.channel_sender = 2;

                    _ctx.pstg_WorkOrders.Add(item);
                    await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return workorders;
        }


        public async Task<List<pstg_WorkOrderSparePart>> CreateListWorkOrderSparePartAsync(List<pstg_WorkOrderSparePart> workorderspareparts)
        {
            try
            {
                foreach (var item in workorderspareparts)
                {
                    item.send_date = DateTime.Now;
                    item.send_status = true;
                    item.channel_sender = 2;

                    _ctx.pstg_WorkOrderSpareParts.Add(item);
                    await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return workorderspareparts;
        }


        public async Task<List<pstg_SparePartSale>> CreateListSparePartSaleAsync(List<pstg_SparePartSale> sparepartsales)
        {
            try
            {
                foreach (var item in sparepartsales)
                {
                    item.send_date = DateTime.Now;
                    item.send_status = true;
                    item.channel_sender = 2;

                    _ctx.pstg_SparePartSales.Add(item);
                    await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return sparepartsales;
        }


        public async Task<List<pstg_SparePartStock>> CreateListSparePartStockAsync(List<pstg_SparePartStock> sparepartstocks)
        {
            try
            {
                foreach (var item in sparepartstocks)
                {
                    item.send_date = DateTime.Now;
                    item.send_status = true;
                    item.channel_sender = 2;

                    _ctx.pstg_SparePartStocks.Add(item);
                    await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return sparepartstocks;
        }

        public async Task<pstg_SparePartStock> CreateSparePartStockAsync(pstg_SparePartStock sparepartstock)
        {
            //if(await UserExists(sparepartstock.dealer_id.ToString()))
            //    return null;
            _ctx.pstg_SparePartStocks.Add(sparepartstock);
            await _ctx.SaveChangesAsync();
            return sparepartstock;
        }
    }
}