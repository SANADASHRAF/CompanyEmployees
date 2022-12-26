 using Contracts;
using Repository;
using Service.Contracts;
using Service;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployees.Extensions
{
   public static class ServiceExtensions
    {
        
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

        //factory manager
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

        //for  service layer
        public static void ConfigureServiceManager(this IServiceCollection services) =>
                services.AddScoped<IServiceManager, ServiceManager>();


        //RepositoryContextFactory registered our RepositoryContext class at design time
        // this for RepositoryManager service registration, which happens at runtime
        //to solve problem of RepositoryManagerservice while haappen at runtime and the RepositoryContext happen at designime
        public static void ConfigureSqlContext(this IServiceCollection services,IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));



    }
}
