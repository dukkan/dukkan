using Dukkan.Configuration;
using Dukkan.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Dukkan.Catalog.Data
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class DukkanCatalogDbContextFactory : IDesignTimeDbContextFactory<DukkanCatalogDbContext>
    {
        public DukkanCatalogDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DukkanCatalogDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            DukkanCatalogDbContextConfigurer.Configure(builder, configuration.GetConnectionString(DukkanConsts.ConnectionStringName));

            return new DukkanCatalogDbContext(builder.Options);
        }
    }
}
