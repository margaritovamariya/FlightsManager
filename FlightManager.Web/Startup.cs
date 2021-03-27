using FlightManager.Data;
using FlightManager.Models;
using FlightManager.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FlightManager.Web
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
            //Register DBContext
            services.AddDbContext<FlightManagerDbContext>(options =>
            {
                options.UseSqlServer(this.Configuration["ConnectionString"]);
            });
            //Add Identity
            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<FlightManagerDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();


            //Here we register the services which we already created in FlightManager.Services
            services.AddTransient<IFlightService, FlightService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IUserServices, UserServices>();
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
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
