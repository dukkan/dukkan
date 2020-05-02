using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Dukkan.Web
{
    [DependsOn(
       typeof(DukkanWebCoreModule)
    )]
    public class DukkanWebHostModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DukkanWebHostModule).GetAssembly());
        }
    }
}
