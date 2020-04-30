using Abp.Zero.EntityFrameworkCore;
using Dukkan.Authorization.Roles.Domain;
using Dukkan.Authorization.Users.Domain;
using Dukkan.MultiTenancy.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dukkan.Data
{
    public class DukkanZeroDbContext : AbpZeroDbContext<Tenant, Role, User, DukkanZeroDbContext>
    {
        public DukkanZeroDbContext(DbContextOptions<DukkanZeroDbContext> options)
            : base(options)
        {
        }
    }
}
