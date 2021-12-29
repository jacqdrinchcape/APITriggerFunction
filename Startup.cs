using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using APITriggerFunction;
using APITriggerFunction.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(APITriggerFunction.Startup))]

namespace APITriggerFunction
{
    public class Startup : FunctionsStartup
    {

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var tempProvider = builder.Services.BuildServiceProvider();
            var config = tempProvider.GetRequiredService<IConfiguration>();

            //var connectionString = "Server=tcp:az-sbr-br-sql-dev-asp-02.database.windows.net,1433;Initial Catalog=az-sbr-br-sqldb-dev-asp-02;Persist Security Info=False;User ID=ddcadmindb;Password=CkoUW533YMHvAwRi;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            builder.Services.AddDbContext<DataContext>(x =>
            {
                x.UseSqlServer(config["SqlServerConnection"]
                , options=>options.EnableRetryOnFailure());
            });


            builder.Services.AddTransient<ICRUDService, CRUDService>();

        }
    }
}