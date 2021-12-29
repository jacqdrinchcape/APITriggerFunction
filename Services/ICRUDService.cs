using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APITriggerFunction.Model;

namespace APITriggerFunction.Services
{
    public interface ICRUDService
    {
        Task<List<pstg_VehicleSale>> CreateListVehicleSaleAsync(List<pstg_VehicleSale> vehiclesales);
        Task<List<pstg_WorkOrder>> CreateListWorkOrderAsync(List<pstg_WorkOrder> workorders);
        Task<List<pstg_WorkOrderSparePart>> CreateListWorkOrderSparePartAsync(List<pstg_WorkOrderSparePart> workorderspareparts);
        Task<List<pstg_SparePartSale>> CreateListSparePartSaleAsync(List<pstg_SparePartSale> sparepartsales);
        Task<List<pstg_SparePartStock>> CreateListSparePartStockAsync(List<pstg_SparePartStock> sparepartstocks);

        Task<pstg_SparePartStock> CreateSparePartStockAsync(pstg_SparePartStock sparepartstock);
    }
}