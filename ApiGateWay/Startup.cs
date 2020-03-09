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
using InfrastructureBase.EfDataAccess;
using Microsoft.EntityFrameworkCore;

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
            services.AddTransient<IStartupFilter, GlobalRequestFilter>();
            services.AddHostedService<CustomerService>();
            services.AddCors(options =>
            {
                options.AddPolicy("cors",
                    builder => { builder.WithOrigins("http://www.oxygen-eshopsample.com").AllowAnyHeader().AllowAnyMethod().AllowCredentials(); });

            });

            services.AddControllers(x=>x.Filters.Add(typeof(GlobalExceptionFilter))).AddControllersAsServices()
                .AddNewtonsoftJson(option =>
                {
                    option.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                })
                ;
            services.AddDbContext<DefContext>(options => options.UseSqlServer(Configuration.GetSection("SqlConnectionString").Value));//����Ǩ��
            services.AddCap(x =>
            {
                x.UseSqlServer(Configuration.GetSection("SqlConnectionString").Value);
                x.UseRabbitMQ(Configuration.GetSection("RabbitMqConnection").Value);
            });
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //ע��oxygen����
            builder.RegisterOxygen();
            builder.RegisterModule(new ConfigurationModule(Configuration));
            builder.RegisterBaseModule();//ע��ۺϷ���
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //�����м����������Swagger��ΪJSON�ս��
            app.UseSwagger();
            //�����м�������swagger-ui��ָ��Swagger JSON�ս��
            app.UseSwaggerUI(c =>
            {
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.SwaggerEndpoint("/myswagger.json", "My API V1");
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });
            app.UseRouting();
            app.UseCors("cors");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
