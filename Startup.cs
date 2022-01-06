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

            builder.Services.AddDbContext<DataContext>(x =>
            {
                x.UseSqlServer(config["SqlServerConnection"]
                , options=>options.EnableRetryOnFailure());
            });


            builder.Services.AddTransient<ICRUDService, CRUDService>();

        }
    }
}