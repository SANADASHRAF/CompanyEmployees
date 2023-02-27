 using Contracts;
using Repository;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace CompanyEmployees.Extensions
{
   public static class ServiceExtensions
    {
        

        //for cors
        public static void ConfigureCors(this IServiceCollection services) =>
         services.AddCors(options =>
         {
             options.AddPolicy("CorsPolicy", builder =>
             builder.AllowAnyOrigin() //withOrigin()
             .AllowAnyMethod()//WithMethods()
             .AllowAnyHeader());//WithHeaders()
         });

       public static void ConfigureIISIntegration(this IServiceCollection services) =>
         services.Configure<IISOptions>(options =>
         {

         });

        //repository manager
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();


        //RepositoryContextFactory registered our RepositoryContext class at design time
        // this for RepositoryManager service registration, which happens at runtime
        //to solve problem of RepositoryManagerservice while haappen at runtime and the RepositoryContext happen at designime
        public static void ConfigureSqlContext(this IServiceCollection services,IConfiguration configuration) =>
        services.AddSqlServer<RepositoryContext>((configuration.GetConnectionString("sqlConnection")));



        //for Identity
        public static void ConfigureIdentity(this IServiceCollection services) 
        {
            var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
               

            })
            
             .AddEntityFrameworkStores<RepositoryContext>()
             .AddDefaultTokenProviders();
                
        }


        // for JWT
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = Environment.GetEnvironmentVariable("SECRET");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new
                    SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }


    }
}
