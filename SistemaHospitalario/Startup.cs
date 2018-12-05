using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SistemaHospitalario.Models;

namespace SistemaHospitalario
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<DbContext2>(options=> {
                options.UseSqlServer("Server=tcp:sistemahosp.database.windows.net,1433;Initial Catalog=SistemaHospAzure;Persist Security Info=False;User ID=sistemahost;Password=sistemaH)$pital;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            });
            services.AddSession(options =>
            {
                options.Cookie.Name = ".SistemaHospital.Session";
                options.IdleTimeout = TimeSpan.FromHours(12);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(config=>config.MapRoute(
                "Default",
                "{controller}/{action}/{id?}",
                new {
                    Controller="Home",
                    Action="Index"
                }
            ));

            

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Ups, la base de datos no existe");
            });
        }

    }
}
