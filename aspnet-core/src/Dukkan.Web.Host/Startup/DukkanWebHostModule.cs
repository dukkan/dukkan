using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Dukkan.Web.Configuration;

namespace Dukkan.Web.Startup
{
    [DependsOn(
       typeof(DukkanWebCoreModule)
    )]
    public class DukkanWebHostModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public DukkanWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DukkanWebHostModule).GetAssembly());
        }
    }
}
