using System.Linq;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.AutoMapper;
using AutoMapper;
using Dukkan.Authorization.Roles.Domain;
using Dukkan.Authorization.Roles.Dto;
using Dukkan.Authorization.Users.Domain;
using Dukkan.Authorization.Users.Dto;
using Dukkan.MultiTenancy.Domain;
using Dukkan.MultiTenancy.Dto;

namespace Dukkan
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression config, MultiLingualMapContext multiLingualMapContext)
        {
            CreateMultiTenancyMaps(config);
            CreateAuthorizationMaps(config);
        }

        private static void CreateMultiTenancyMaps(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<TenantCreateDto, Tenant>();
            configuration.CreateMap<Tenant, TenantDto>();
        }

        private static void CreateAuthorizationMaps(IMapperConfigurationExpression configuration)
        {
            // User
            configuration.CreateMap<User, UserDto>();
            configuration.CreateMap<UserDto, User>()
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore());
            configuration.CreateMap<UserCreateDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

            // Role and permission
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<Role, RoleDto>().ForMember(x => x.GrantedPermissions,
                opt => opt.MapFrom(x => x.Permissions.Where(p => p.IsGranted)));
            configuration.CreateMap<RoleDto, Role>();
            configuration.CreateMap<RoleCreateDto, Role>();
            configuration.CreateMap<Role, RoleEditDto>();
            configuration.CreateMap<Permission, PermissionDto>();
            configuration.CreateMap<Permission, PermissionFlatDto>();
            configuration.CreateMap<Permission, string>().ConvertUsing(r => r.Name);
            configuration.CreateMap<RolePermissionSetting, string>().ConvertUsing(r => r.Name);
        }
    }
}