using Assignment10.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10
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
            services.AddControllersWithViews();

            // add service to configure sqlite db that we connected
            services.AddDbContext<BowlingLeagueContext>(options =>
               options.UseSqlite(Configuration["ConnectionStrings:BowlingLeagueDbConnection"])
           );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //create endpoints for url style purposes and make it easier to search and filter via url
                endpoints.MapControllerRoute("teamNamepagenum",
                    "Team/{team}/{teamName}/{pageNum}",
                    new { Controller = "Home", Action = "ContactList", pageNum = 1 }
                );

                endpoints.MapControllerRoute("pageNum",
                    "{pageNum}",
                    new { Controller = "Home", Action = "ContactList" }
                );

                endpoints.MapControllerRoute("team",
                    "Team/{team}/{teamName}",
                    new { Controller = "Home", Action = "ContactList" }
                );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
