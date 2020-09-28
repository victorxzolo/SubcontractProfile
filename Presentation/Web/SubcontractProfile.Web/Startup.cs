using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;

namespace SubcontractProfile.Web
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
            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Redis implementation of IDistributedCache.
            // This will override any previously registered IDistributedCache service.
          //  services.AddSingleton<IDistributedCache, RedisCache>();


            services.AddControllersWithViews();
            services.AddRazorPages();

            #region Localization

            services.AddLocalization(options => options.ResourcesPath = "Resource");
            services.AddMvc()
              .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
              .AddDataAnnotationsLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { new CultureInfo("th"), new CultureInfo("en") };
                options.DefaultRequestCulture = new RequestCulture("th", "th");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

          

            #endregion



            // Add framework services.
            services.AddMvc()
                    .AddControllersAsServices();

            // Pascal casing
            services.AddControllersWithViews().
            AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            

        }

      

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
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

            #region Localization

            var supportedCultures = new[] { "th", "en" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture("th")
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            app.UseRequestLocalization(localizationOptions);


            #endregion



            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                     //pattern: "{controller=Home}/{action=Index}/{id?}");
                      pattern: "{controller=Account}/{action=Login}/{id?}"); //สำหรับ subcontract portal
                  //  pattern: "{controller=Registration}/{action=SearchCompanyVerify}/{id?}"); //สำหรับ fbbportal
        });

            loggerFactory.AddFile("Logs/SubcontractProfileLog-{Date}.txt");

            app.UseRequestLocalization();
        }
    }
}
