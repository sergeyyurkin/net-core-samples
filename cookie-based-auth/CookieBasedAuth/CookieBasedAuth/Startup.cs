using System.Security.Claims;
using CookieBasedAuth.Authorization.Handlers;
using CookieBasedAuth.Authorization.Requirements;
using CookieBasedAuth.Data;
using CookieBasedAuth.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CookieBasedAuth
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<UserContext>(option => option.UseSqlServer(connection));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/account/login");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/account/login");
                });

            // Add AgeHandler in service collection.
            services.AddTransient<IAuthorizationHandler, AgeHandler>();

            services.AddAuthorization(options =>
            {
                // Add castom simple policy in authorization.
                options.AddPolicy(AppAuthPolicy.OnlyForLondon, policy =>
                {
                    policy.RequireClaim(ClaimTypes.Locality, "London");
                });
                options.AddPolicy(AppAuthPolicy.OnlyForMicrosoft, policy =>
                {
                    policy.RequireClaim(AppClaimTypes.Company, "Microsoft");
                });

                // Add user age limit policy in authorization.
                options.AddPolicy(AppAuthPolicy.UserAgeLimit, policy =>
                {
                    policy.Requirements.Add(new AgeRequirement(18));
                });
            });

            services.AddControllersWithViews();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
