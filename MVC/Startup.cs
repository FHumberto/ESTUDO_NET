using System.Globalization;
using CNA_SalesWebMvc.Data;
using CNA_SalesWebMvc.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

namespace CNA_SalesWebMvc
{
    public class Startup
    {
        public IConfiguration CFG { get; }

        public Startup(IConfiguration configuration)
        {
            CFG = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SalesWebMvcContext>
                (options => options.UseSqlServer(CFG.GetConnectionString("DefaultConnection")));
            services.AddScoped<SellerService>();
            services.AddScoped<DepartmentService>();
            services.AddScoped<SalesRecordService>();
            services.AddRazorPages();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // DEFINE A LOCALIZAÇÃO DA APLICAÇÃO
            var enUS = new CultureInfo("en-US");

            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUS),
                SupportedCultures = new List<CultureInfo> { enUS },
                SupportedUICultures = new List<CultureInfo> { enUS }
            };

            // passa a localização como argumento
            app.UseRequestLocalization(localizationOptions);

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
            app.Run();
        }
    }
}