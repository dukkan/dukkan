using Abp.AspNetCore;
using Abp.AutoMapper;
using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Dukkan.Configuration;
using Dukkan.Localization;
using Dukkan.Timing;

namespace Dukkan
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(AbpAspNetCoreModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class DukkanCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;
            Configuration.MultiTenancy.IsEnabled = DukkanConsts.MultiTenancyEnabled;
            Configuration.Settings.Providers.Add<AppSettingProvider>();
            DukkanLocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DukkanCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}