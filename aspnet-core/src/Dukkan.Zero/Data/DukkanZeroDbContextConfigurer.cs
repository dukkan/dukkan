using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Dukkan.Data
{
    public static class DukkanZeroDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<DukkanZeroDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<DukkanZeroDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
