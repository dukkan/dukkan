using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Dukkan.Authorization.Roles;
using Dukkan.Authorization.Users;
using Dukkan.MultiTenancy;
using Dukkan.Catalog;

namespace Dukkan.EntityFrameworkCore
{
    public class DukkanDbContext : AbpZeroDbContext<Tenant, Role, User, DukkanDbContext>
    {
        public DukkanDbContext(DbContextOptions<DukkanDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
    }
}
