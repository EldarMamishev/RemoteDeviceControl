using System.Text;
using AutoMapper;
using Core.Entities.ApplicationIdentity;
using Core.Entities.ApplicationIdentity.Access;
using Data;
using Data.Contracts;
using Data.Contracts.DataAccess;
using Data.DataAccess;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Facades;
using Services.Facades.Base;
using WebApi.Helpers.Token;
using WebApi.Hubs;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                    .UseLazyLoadingProxies();
            });

            ConfigureAuthentication(services);

            ConfigureDI(services);

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllersWithViews();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            }); 
            
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chatter");
            });
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new PathString("/Authentication/Login");
                });

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]));

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = signingKey,
                        ValidateAudience = true,
                        ValidAudience = Configuration["Tokens:Audience"],
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Tokens:Issuer"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });

            services.AddAuthorization();
        }

        private void ConfigureDI(IServiceCollection services)
        {
            services.AddScoped<DbContext, AppDbContext>();
            services.AddScoped<IDataInitializer, DataInitializer>();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPersonMappersFacade, PersonMappersFacade>();
        }
    }
}
