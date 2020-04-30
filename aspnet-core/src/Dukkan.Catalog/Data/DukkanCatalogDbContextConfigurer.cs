using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Dukkan.Catalog.Data
{
    public static class DukkanCatalogDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<DukkanCatalogDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<DukkanCatalogDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
