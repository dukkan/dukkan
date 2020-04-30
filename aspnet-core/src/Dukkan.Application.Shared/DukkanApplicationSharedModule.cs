using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Dukkan
{
    [DependsOn(typeof(DukkanCoreSharedModule))]
    public class DukkanApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DukkanApplicationSharedModule).GetAssembly());
        }
    }
}