using Abp.AspNetCore.Configuration;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.EntityFrameworkCore.Configuration;
using Abp.IdentityServer4;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using Abp.Zero.EntityFrameworkCore;
using Dukkan.Authorization.Roles;
using Dukkan.Authorization.Roles.Domain;
using Dukkan.Authorization.Users.Domain;
using Dukkan.Data;
using Dukkan.MultiTenancy.Domain;

namespace Dukkan
{
    [DependsOn(
        typeof(AbpZeroCoreEntityFrameworkCoreModule),
        typeof(AbpZeroCoreIdentityServerEntityFrameworkCoreModule),
        typeof(DukkanZeroSharedModule)
    )]
    public class DukkanZeroModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }
        
        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<DukkanZeroDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        DukkanZeroDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        DukkanZeroDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
            
            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            // Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.Modules.AbpAutoMapper().Configurators.Add(configuration =>
            {
                CustomDtoMapper.CreateMappings(configuration, new MultiLingualMapContext(
                    IocManager.Resolve<ISettingManager>()
                ));
            });

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(typeof(DukkanZeroModule).GetAssembly());
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DukkanZeroModule).GetAssembly());
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