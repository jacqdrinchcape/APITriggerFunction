using Microsoft.EntityFrameworkCore;
using APITriggerFunction.Model;

namespace APITriggerFunction
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options){}

        public DbSet<pstg_VehicleSale> pstg_VehicleSales { get; set; }
        public DbSet<pstg_WorkOrder> pstg_WorkOrders { get; set; }
        public DbSet<pstg_WorkOrderSparePart> pstg_WorkOrderSpareParts { get; set; }
        public DbSet<pstg_SparePartSale> pstg_SparePartSales { get; set; }
        public DbSet<pstg_SparePartStock> pstg_SparePartStocks { get; set; }
        public DbSet<pstg_ErrorLog> pstg_ErrorLogs { get; set; }
        public DbSet<VehicleIdentificationNumber> VehicleIdentificationNumbers { get; set; }
        
    }
}