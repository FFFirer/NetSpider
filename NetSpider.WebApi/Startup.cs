using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NetSpider.Domain.Ctrip;
using Microsoft.EntityFrameworkCore;
using NetSpider.WebApi.Services;
using AutoMapper;

namespace NetSpider.WebApi
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
            services.AddLogging(logbuilder =>
            {
                logbuilder.ClearProviders();
                logbuilder.AddNLog("NLog.config");
            });
            services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.ConfigureAutoMapper(); // ºØ÷–µƒ≈‰÷√AutoMapper
            services.AddDbContext<CtripDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("MysqlCtrip")));
            services.AddScoped<CtripServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
