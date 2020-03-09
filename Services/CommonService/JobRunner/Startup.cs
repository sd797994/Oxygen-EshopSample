using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using InfrastructureBase.EfDataAccess;
using JobRunner.EventHandler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Oxygen;

namespace JobRunner
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureOxygen(Configuration);
            services.AddLogging(configure =>
            {
                configure.AddConfiguration(Configuration.GetSection("Logging"));
                configure.AddConsole();
            });
            services.AddAutofac();
            GlobalConfiguration.Configuration.UseRedisStorage(Configuration.GetSection("RedisConnection").Value);
            services.AddHangfire(x => { });
            services.RegisterRunnerModule();//注入订阅作业入口
            services.AddDbContext<DefContext>(options => options.UseSqlServer(Configuration.GetSection("SqlConnectionString").Value));//数据迁移
            services.AddHostedService<CustomerService>();
            services.AddCap(x =>
            {
                x.UseSqlServer(Configuration.GetSection("SqlConnectionString").Value);
                x.UseRabbitMQ(Configuration.GetSection("RabbitMqConnection").Value);
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //注入oxygen服务
            builder.RegisterOxygen();
            builder.RegisterModule(new ConfigurationModule(Configuration));
            builder.RegisterBaseModule();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHangfireServer();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => await context.Response.WriteAsync("Job is Runing!"));
            });
        }
    }
}
