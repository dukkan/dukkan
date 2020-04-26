using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.AutoMapper;
using AutoMapper;
using Dukkan.Authorization.Roles;
using Dukkan.Authorization.Users;
using Dukkan.Catalog;
using Dukkan.Catalog.Dto;
using Dukkan.MultiTenancy;
using Dukkan.MultiTenancy.Dto;
using Dukkan.Roles.Dto;
using Dukkan.Sessions.Dto;
using Dukkan.Users.Dto;
using System.Linq;

namespace Dukkan
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression config, MultiLingualMapContext multiLingualMapContext)
        {
            CreateMultiTenancyMaps(config);
            CreateAuthorizationMaps(config);
            CreateSessionsMaps(config);
            CreateCatalogMaps(config, multiLingualMapContext);
        }

        private static void CreateMultiTenancyMaps(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateTenantDto, Tenant>();
            configuration.CreateMap<Tenant, TenantDto>();
        }

        private static void CreateAuthorizationMaps(IMapperConfigurationExpression configuration)
        {
            // User
            configuration.CreateMap<User, UserDto>();
            configuration.CreateMap<UserDto, User>()
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore());
            configuration.CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

            // Role and permission
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<Role, RoleDto>().ForMember(x => x.GrantedPermissions,
                opt => opt.MapFrom(x => x.Permissions.Where(p => p.IsGranted)));
            configuration.CreateMap<RoleDto, Role>();
            configuration.CreateMap<CreateRoleDto, Role>();
            configuration.CreateMap<Role, RoleEditDto>();
            configuration.CreateMap<Permission, PermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, string>().ConvertUsing(r => r.Name);
            configuration.CreateMap<RolePermissionSetting, string>().ConvertUsing(r => r.Name);
        }

        private static void CreateSessionsMaps(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
        }

        private static void CreateCatalogMaps(IMapperConfigurationExpression configuration, MultiLingualMapContext multiLingualMapContext)
        {
            configuration.CreateMap<Category, CategoryListDto>();
            configuration.CreateMap<Category, CategoryEditDto>();
            configuration.CreateMap<CategoryEditDto, Category>()
                .ForMember(x => x.Translations, opt => opt.Ignore());
            configuration.CreateMap<CategoryTranslation, CategoryTranslationEditDto>().ReverseMap();
            configuration.CreateMultiLingualMap<Category, CategoryTranslation, CategoryListDto>(multiLingualMapContext);
        }
    }
}