using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Dukkan
{
    public class DukkanCoreSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DukkanCoreSharedModule).GetAssembly());
        }
    }
}