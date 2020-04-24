using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Dukkan.Authorization;

namespace Dukkan
{
    [DependsOn(
        typeof(DukkanApplicationSharedModule),
        typeof(DukkanCoreModule)
    )]
    public class DukkanApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<DukkanAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(DukkanApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}