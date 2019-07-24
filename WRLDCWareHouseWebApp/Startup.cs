using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WRLDCWareHouseWebApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using WRLDCWareHouseWebApp.Services;
using WRLDCWarehouse.Data;
using Microsoft.Extensions.Logging;
using System.IO;

namespace WRLDCWareHouseWebApp
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("WarehouseConnection"), b => b.MigrationsAssembly("WRLDCWareHouseWebApp")));

            services.AddDbContext<WRLDCWarehouseDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("WarehouseConnection"), b => b.MigrationsAssembly("WRLDCWareHouseWebApp")));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // get the path of common documents folder "C:\Users\Public\Documents"
            string commonDocsFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            if (Directory.Exists(commonDocsFolder))
            {
                // configure logger factory for text file logs
                string logsFolder = "Logs";
                string logFilename = "wrldcWarehouseLogs-{Date}.txt";
                string logFilePath = Path.Combine(new string[] { commonDocsFolder, logsFolder, logFilename });
                loggerFactory.AddFile(logFilePath, LogLevel.Critical);
            }
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
