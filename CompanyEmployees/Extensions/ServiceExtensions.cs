 using Contracts;
using Repository;
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

        //repository manager
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();


        //RepositoryContextFactory registered our RepositoryContext class at design time
        // this for RepositoryManager service registration, which happens at runtime
        //to solve problem of RepositoryManagerservice while haappen at runtime and the RepositoryContext happen at designime
        
public static void ConfigureSqlContext(this IServiceCollection services,IConfiguration configuration) =>
        services.AddSqlServer<RepositoryContext>((configuration.GetConnectionString("sqlConnection")));


    }
}
