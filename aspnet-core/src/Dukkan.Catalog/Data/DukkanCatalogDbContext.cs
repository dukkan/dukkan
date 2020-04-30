using Abp.EntityFrameworkCore;
using Dukkan.Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dukkan.Catalog.Data
{
    public class DukkanCatalogDbContext : AbpDbContext
    {
        public DukkanCatalogDbContext(DbContextOptions<DukkanCatalogDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
    }
}
