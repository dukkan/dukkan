using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using Dukkan.Authorization.Users.Domain;
using Dukkan.Editions.Domain;

namespace Dukkan.MultiTenancy.Domain
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository,
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore)
            : base(
                tenantRepository,
                tenantFeatureRepository,
                editionManager,
                featureValueStore)
        {
        }
    }
}