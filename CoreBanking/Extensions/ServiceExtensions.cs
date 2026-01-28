using CoreBanking.Contracts;
using CoreBanking.Data;
using CoreBanking.Entites;
using CoreBanking.Helpers;
using CoreBanking.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CoreBanking.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
            });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerService, LoggerService>();

        //public static void ConfigureIdentity(this IServiceCollection services)
        //{
        //    services.AddIdentity<Customer, IdentityRole>(opt =>
        //    {
        //        opt.Password.RequiredLength = 6;
        //        opt.User.RequireUniqueEmail = true;
        //    }).AddEntityFrameworkStores<BankContext>();
        //}

        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JWTSettings");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
                };
            });
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BankContext>(opts =>
              opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<JwtHelper>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IReferenceSequenceService, ReferenceSequenceService>();
            services.AddScoped<ITransferService, TransferService>();
            services.AddScoped<ITransactionService, TransactionService>();
        }

    }
}
