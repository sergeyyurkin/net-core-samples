using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTBasedAuth.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace JWTBasedAuth
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Указывает, будет ли валидироваться издатель токена.
                        ValidateIssuer = true,

                        // Представляет издателья.
                        ValidIssuer = AuthOptions.ISSUER,

                        // Указывает, будет ли валидироваться потрелитель токена.
                        ValidateAudience = true,

                        // Представляет потрелителя токена.
                        ValidAudience = AuthOptions.AUDIENCE,

                        // Указывает, будет ли валидироваться время существования токена.
                        ValidateLifetime = true,

                        // Представляет ключ безопасности.
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),

                        // Указывает, будет ли валидироваться ключ безопасности.
                        ValidateIssuerSigningKey = true
                    };
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

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
