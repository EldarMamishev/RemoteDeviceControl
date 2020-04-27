using System.Text;
using AutoMapper;
using Core.Entities.Users;
using Core.Entities.Users.Access;
using Data;
using Data.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories;
using Repositories.DataAccess;
using WebApi.Helpers.Token;

namespace WebApi.Startup
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
            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<AppDbContext>(opt => 
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            ConfigureAuthentication(services);

            ConfigureDI(services);

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllers();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

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
        }

        private void ConfigureDI(IServiceCollection services)
        {
            services.AddScoped<DbContext, AppDbContext>();
            services.AddScoped<IDataInitializer, DataInitializer>();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
