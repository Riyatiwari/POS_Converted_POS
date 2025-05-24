using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Converted_POS.Models;
using Converted_POS.Services;
using Converted_POS.Services.Implementations;
using Converted_POS.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Converted_POS
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
            // Add Database context
            services.AddDbContext<Data.ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                
            // Register services
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<Services.ITillSummaryService, Services.Implementations.TillSummaryService>();
            services.AddScoped<IProductSummaryService, ProductSummaryService>();
            services.AddScoped<Services.Interfaces.ITransactionReportService, Services.Implementations.TransactionReportService>();
            services.AddScoped<Services.ITransactionReportService, Services.Implementations.TransactionReportService>();
            services.AddScoped<IBindingService, BindingService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IZReportService, ZReportService>();

            // Add logging
            services.AddLogging(logging =>
            {
                logging.AddConsole();
                logging.AddDebug();
            });

            // Configure mail settings
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            services.AddSession();
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDistributedMemoryCache();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //  services.AddMvc().AddSessionStateTempDataProvider();
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
            services.AddControllersWithViews();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromHours(1);  
                //options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddMvc()
             .AddNewtonsoftJson(options =>
           {
               options.SerializerSettings.ContractResolver = new DefaultContractResolver();
               options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented; // Optional: Indented JSON
           });
            //.AddNewtonsoftJson(options =>
            //{
            //    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            //});
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            services.AddHttpContextAccessor();
            services.AddScoped<Converted_POS.Controls.menuaccordian>();
            services.AddSingleton(serviceProvider =>
            {
                var binding = new BasicHttpBinding
                {
                    CloseTimeout = TimeSpan.FromMinutes(1),
                    OpenTimeout = TimeSpan.FromMinutes(1),
                    ReceiveTimeout = TimeSpan.FromMinutes(10),
                    SendTimeout = TimeSpan.FromMinutes(10),
                    MaxBufferPoolSize = int.MaxValue,
                    MaxBufferSize = int.MaxValue,
                    MaxReceivedMessageSize = int.MaxValue,
                    ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas
                    {
                        MaxDepth = int.MaxValue,
                        MaxStringContentLength = int.MaxValue,
                        MaxArrayLength = int.MaxValue,
                        MaxBytesPerRead = int.MaxValue,
                        MaxNameTableCharCount = int.MaxValue
                    }
                };

                var endpoint = new EndpointAddress("https://localhost:44336/WebService/MyWCFService");
                return new ServiceReference1.WebServiceSoapClient(binding, endpoint);
            });
        }
        //services.AddCors(options =>
        //    {
        //        options.AddPolicy("AllowAll", builder =>
        //            builder.AllowAnyOrigin()
        //                   .AllowAnyMethod()
        //                   .AllowAnyHeader());
        //    });



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //Specify the MyCustomPage1.html as the default page
            DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("/SignIn");
            //Setting the Default Files
            app.UseDefaultFiles(defaultFilesOptions);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
            //app.UseAuthorization();
           
            app.UseMvc();
            app.UseCors("AllowAll");
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
