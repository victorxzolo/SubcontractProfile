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
using System.Collections.Generic;
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

            //services.AddLocalization(options => options.ResourcesPath = "Resources");
            //services.AddMvc()
            //    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
            //    .AddDataAnnotationsLocalization();

            //services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

 
        //    services.Configure<RequestLocalizationOptions>(
        //opts =>
        //{
        //    var supportedCultures = new List<CultureInfo>
        //    {
        //        new CultureInfo("th-TH"),
        //        new CultureInfo("en-US"),
        //        new CultureInfo("en")

        //    };

        //    opts.DefaultRequestCulture = new RequestCulture("en-US");
        //    // Formatting numbers, dates, etc.
        //    opts.SupportedCultures = supportedCultures;
        //    // UI strings that we have localized.
        //    opts.SupportedUICultures = supportedCultures;
        //});

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

            var supportedCultures = new[] { new CultureInfo("th"), new CultureInfo("en") };
            //var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture("th")
            //    //.AddSupportedCultures(supportedCultures)
            //    //.AddSupportedUICultures(supportedCultures);
            //app.UseRequestLocalization(localizationOptions);
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("th"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            #endregion



            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseSession();

            //var localizationOption = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            //app.UseRequestLocalization(localizationOption.Value);



            //// กำหนด Culture ที่เว็บจะรองรับ
            //var cultures = new List<CultureInfo>
            //{
            //    new CultureInfo("en-ES"),
            //    new CultureInfo("th-TH")
            //};

            //// กำหนด Default Culture กำหนดเป็นภาษาอังกฤษครับ 
            //// พารามิเตอร์แรกจะเป็น culture ถัดมาจะเป็น ui-culture
            //var requestCulture = new RequestCulture("th-TH", "th-TH");

            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = requestCulture,
            //    SupportedCultures = cultures,
            //    SupportedUICultures = cultures,
            //    RequestCultureProviders = new List<IRequestCultureProvider>
            //    {
            //        new CookieRequestCultureProvider
            //        {
            //  // กำหนดชื่อของ Cookie ที่จะให้เก็บค่าของภาษาที่เลือกครับ
            //            CookieName = "Web.Language"
            //        }
            //    }
            //});


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                     //pattern: "{controller=Home}/{action=Index}/{id?}");
                     pattern: "{controller=Account}/{action=Login}/{id?}"); //สำหรับ subcontract portal
                                                                            //   pattern: "{controller=LogonByUser}/{action=LogonByUser}/{id?}"); //สำหรับ fbbportal
                });



            loggerFactory.AddFile("Logs/SubcontractProfileLog-{Date}.txt");

            app.UseRequestLocalization();
        }
    }
}
