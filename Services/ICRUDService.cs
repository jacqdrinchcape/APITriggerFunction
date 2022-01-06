using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APITriggerFunction.Model;

namespace APITriggerFunction.Services
{
    public interface ICRUDService
    {
        Task<List<pstg_VehicleSaleResp>> CreateListVehicleSaleAsync(List<pstg_VehicleSale> vehiclesales);
        Task<List<pstg_WorkOrderResp>> CreateListWorkOrderAsync(List<pstg_WorkOrder> workorders);
        Task<List<pstg_WorkOrderSparePartResp>> CreateListWorkOrderSparePartAsync(List<pstg_WorkOrderSparePart> workorderspareparts);
        Task<List<pstg_SparePartSaleResp>> CreateListSparePartSaleAsync(List<pstg_SparePartSale> sparepartsales);
        Task<List<pstg_SparePartStockResp>> CreateListSparePartStockAsync(List<pstg_SparePartStock> sparepartstocks);

        Task<pstg_ErrorLog> CreateErrorLogAsync(pstg_ErrorLog errorlog);

        Task<pstg_SparePartStock> CreateSparePartStockAsync(pstg_SparePartStock sparepartstock);
    }
}