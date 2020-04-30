using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Dukkan.Configuration;
using Dukkan.Web;

namespace Dukkan.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class DukkanDbContextFactory : IDesignTimeDbContextFactory<DukkanDbContext>
    {
        public DukkanDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DukkanDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DukkanDbContextConfigurer.Configure(builder, configuration.GetConnectionString(DukkanConsts.ConnectionStringName));

            return new DukkanDbContext(builder.Options);
        }
    }
}
