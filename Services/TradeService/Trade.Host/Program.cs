using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Oxygen;
using System;
using System.IO;
using System.Threading.Tasks;
using Trade.Infrastructure.EfDataAccess;
using Trade.Infrastructure.Modules;

namespace Trade.Host
{
    class Program
    {
        private static IConfiguration _configuration { get; set; }
        static async Task Main(string[] args)
        {
            await CreateDefaultHost(args).Build().RunAsync();
        }
        static IHostBuilder CreateDefaultHost(string[] args) => new HostBuilder()
           .ConfigureAppConfiguration((hostContext, config) =>
           {
               config.SetBasePath(Directory.GetCurrentDirectory());
               config.AddJsonFile("appsettings.json");
               config.AddJsonFile("autofac.json");
               config.AddJsonFile("oxygen.json");
               _configuration = config.Build();
           })
           .ConfigureContainer<ContainerBuilder>(builder =>
           {
               //oxygen服务依赖注入
               builder.RegisterOxygen();
               builder.RegisterModule(new ConfigurationModule(_configuration));
           })
           //申明当前应用为Oxygen服务端,默认值为：ConfigureServices
           .UseOxygenService((context, services) =>
           {
               //注入oxygen服务所需配置节
               services.ConfigureOxygen(_configuration);
               services.AddLogging(configure =>
               {
                   configure.AddConfiguration(_configuration.GetSection("Logging"));
                   configure.AddConsole();
               });
               services.AddHostedService<CustomerService>();
               services.AddDbContext<TradeContext>(options => options.UseSqlServer(_configuration.GetSection("modules:2:properties:SqlConnectionString").Value));
               services.AddAutofac();
               services.RegisterCapSubscribe();
               services.AddCap(x =>
               {
                   x.UseEntityFramework<TradeContext>();
                   x.UseRabbitMQ(_configuration.GetSection("modules:2:properties:RabbitMqConnection").Value);
               });
           })
           .UseServiceProviderFactory(new AutofacServiceProviderFactory())
           .UseConsoleLifetime();
    }
}
