using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using Report.Api.BackgroundServices;
using Report.Api.MQServices;
using Report.Domain;
using Report.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Report.Api
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
            services.AddSingleton(sp => new ConnectionFactory() { 
                Uri= new Uri(Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync=true
            });

            services.AddControllers();
            services.AddSingleton<RabbitMQClientService>();
            services.AddSingleton<RabbitMQPublisher>();
            services.AddHostedService<ExcelReportCreatingProcessBackgroundService>();

            services.Configure<DbConfiguration>(options =>
            {
                options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database = Configuration.GetSection("MongoConnection:Database").Value;
            });

            services.AddSingleton<IDbContext<Domain.Report>, DbContext<Domain.Report>>();
            services.AddSingleton<IReportRepository, ReportRepository>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins().AllowAnyOrigin()
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Report.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Report.Api v1"));
            }

            //app.UseHttpsRedirection();

            app.UseCors();

            app.UseFileServer(new FileServerOptions { 
                FileProvider=new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/documents")),
                RequestPath="/wwwroot/documents",
                EnableDefaultFiles=true
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}