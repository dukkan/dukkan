using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Dukkan.Catalog
{
    [DependsOn(
        typeof(DukkanCoreModule)
    )]
    public class DukkanCatalogSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DukkanCatalogSharedModule).GetAssembly());
        }
    }
}