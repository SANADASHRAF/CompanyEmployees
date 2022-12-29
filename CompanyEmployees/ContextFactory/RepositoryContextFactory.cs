//This helps us find the  RepositoryContext class in another project while executing
//we have registered our RepositoryContext class at design time.
// RepositoryManager service registration, which happens at runtime and during that registration, we must have RepositoryContext registered as well in the runtime
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;
namespace CompanyEmployees.ContextFactory
{
//IDesignTimeDbContextFactory interface allows design-time services to discover implementations of this interface
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            //gor connection string
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            //for migration
            var builder = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                b => b.MigrationsAssembly("CompanyEmployees"));

            return new RepositoryContext(builder.Options);
        }
    }


}
