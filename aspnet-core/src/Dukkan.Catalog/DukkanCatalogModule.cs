using Abp.AspNetCore.Configuration;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Dukkan.Catalog.Data;

namespace Dukkan.Catalog
{
    [DependsOn(
        typeof(DukkanCatalogSharedModule)
    )]
    public class DukkanCatalogModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<DukkanCatalogDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        DukkanCatalogDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        DukkanCatalogDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }

            Configuration.Modules.AbpAutoMapper().Configurators.Add(configuration =>
            {
                CustomDtoMapper.CreateMappings(configuration, new MultiLingualMapContext(
                    IocManager.Resolve<ISettingManager>()
                ));
            });

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(typeof(DukkanCatalogModule).GetAssembly());
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DukkanCatalogModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                //SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}