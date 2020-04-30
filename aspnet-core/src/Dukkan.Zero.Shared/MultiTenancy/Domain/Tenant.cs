using Abp.MultiTenancy;
using Dukkan.Authorization.Users.Domain;

namespace Dukkan.MultiTenancy.Domain
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}