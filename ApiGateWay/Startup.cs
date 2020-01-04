using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AggregateServiceManager.ServiceRoute;
using Autofac;
using Autofac.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BaseServcieInterface;
using Oxygen;
using AggregateServiceManager;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Newtonsoft.Json.Serialization;

namespace ApiGateWay
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureOxygen(Configuration);
            services.AddLogging(configure =>
            {
                configure.AddConfiguration(Configuration.GetSection("Logging"));
                configure.AddConsole();
            });
            services.AddAutofac();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHostedService<CustomerService>();
            services.AddCors(options =>
            {
                options.AddPolicy("cors",
                    builder => { builder.WithOrigins("http://www.oxygen-eshopsample.com","http://localhost:8080").AllowAnyHeader().AllowAnyMethod(); });

            });

            services.AddControllers().AddNewtonsoftJson(option => {
                option.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
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
            builder.RegisterBaseModule();//注入聚合服务
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.SwaggerEndpoint("/myswagger.json", "My API V1");
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });
            app.UseRouting();
            app.UseCors("cors");
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
