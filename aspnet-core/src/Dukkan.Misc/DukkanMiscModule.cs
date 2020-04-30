using Abp.AspNetCore.Configuration;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Dukkan
{
    [DependsOn(
        typeof(DukkanMiscSharedModule)
    )]
    public class DukkanMiscModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(configuration =>
            {
                CustomDtoMapper.CreateMappings(configuration, new MultiLingualMapContext(
                    IocManager.Resolve<ISettingManager>()
                ));
            });

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(typeof(DukkanMiscModule).GetAssembly());
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DukkanMiscModule).GetAssembly());
        }
    }
}