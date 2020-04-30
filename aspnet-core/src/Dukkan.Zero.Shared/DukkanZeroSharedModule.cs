using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero;

namespace Dukkan
{
    [DependsOn(
        typeof(AbpZeroCoreModule),
        typeof(DukkanCoreModule)
    )]
    public class DukkanZeroSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DukkanZeroSharedModule).GetAssembly());
        }
    }
}