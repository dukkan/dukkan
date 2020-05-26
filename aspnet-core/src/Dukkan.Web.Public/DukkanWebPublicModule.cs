using Abp.Modules;
using Abp.Reflection.Extensions;
using Dukkan.Configuration;
using Dukkan.Web.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Dukkan.Web
{
    [DependsOn(typeof(DukkanWebCoreModule))]
    public class DukkanWebPublicModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public DukkanWebPublicModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<DukkanNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DukkanWebPublicModule).GetAssembly());
        }
    }
}
