using Abp.AutoMapper;
using Abp.Configuration;
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

            Configuration.Modules.AbpAutoMapper().Configurators.Add(configuration =>
            {
                CustomDtoMapper.CreateMappings(configuration, new MultiLingualMapContext(
                    IocManager.Resolve<ISettingManager>()
                ));
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DukkanApplicationModule).GetAssembly());
        }
    }
}