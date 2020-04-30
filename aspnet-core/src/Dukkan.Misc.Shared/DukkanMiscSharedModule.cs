using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Dukkan
{
    [DependsOn(
        typeof(DukkanZeroSharedModule)
    )]
    public class DukkanMiscSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DukkanMiscSharedModule).GetAssembly());
        }
    }
}