 using Contracts;
using Repository;

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

    }
}
