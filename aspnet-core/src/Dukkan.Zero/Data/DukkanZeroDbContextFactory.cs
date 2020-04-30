using Dukkan.Configuration;
using Dukkan.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Dukkan.Data
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class DukkanDbContextFactory : IDesignTimeDbContextFactory<DukkanZeroDbContext>
    {
        public DukkanZeroDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DukkanZeroDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DukkanZeroDbContextConfigurer.Configure(builder, configuration.GetConnectionString(DukkanConsts.ConnectionStringName));

            return new DukkanZeroDbContext(builder.Options);
        }
    }
}
