using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Dukkan.EntityFrameworkCore
{
    public static class DukkanDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<DukkanDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<DukkanDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
