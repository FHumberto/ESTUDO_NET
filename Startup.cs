using CNA_SalesWebMvc.Data;
using CNA_SalesWebMvc.Services;
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
            services.AddRazorPages();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
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