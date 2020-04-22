using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Dukkan.Authorization.Roles;
using Dukkan.Authorization.Users;
using Dukkan.MultiTenancy;

namespace Dukkan.EntityFrameworkCore
{
    public class DukkanDbContext : AbpZeroDbContext<Tenant, Role, User, DukkanDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public DukkanDbContext(DbContextOptions<DukkanDbContext> options)
            : base(options)
        {
        }
    }
}
