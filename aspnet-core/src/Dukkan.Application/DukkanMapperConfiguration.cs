using Abp.Authorization;
using Abp.Authorization.Roles;
using AutoMapper;
using Dukkan.Authorization.Roles;
using Dukkan.Authorization.Users;
using Dukkan.MultiTenancy;
using Dukkan.MultiTenancy.Dto;
using Dukkan.Roles.Dto;
using Dukkan.Sessions.Dto;
using Dukkan.Users.Dto;
using System.Linq;

namespace Dukkan
{
    public class DukkanMapperConfiguration : Profile
    {
        public DukkanMapperConfiguration()
        {
            CreateMultiTenancyMaps();
            CreateAuthorizationMaps();
            CreateSessionsMaps();
        }

        private void CreateMultiTenancyMaps()
        {
            CreateMap<CreateTenantDto, Tenant>();
            CreateMap<Tenant, TenantDto>();
        }

        private void CreateAuthorizationMaps()
        {
            // User
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore());
            CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

            // Role and permission
            CreateMap<Role, RoleListDto>();
            CreateMap<Role, RoleDto>().ForMember(x => x.GrantedPermissions,
                opt => opt.MapFrom(x => x.Permissions.Where(p => p.IsGranted)));
            CreateMap<RoleDto, Role>();
            CreateMap<CreateRoleDto, Role>();
            CreateMap<Role, RoleEditDto>();
            CreateMap<Permission, PermissionDto>();
            CreateMap<Permission, FlatPermissionDto>();
            CreateMap<Permission, string>().ConvertUsing(r => r.Name);
            CreateMap<RolePermissionSetting, string>().ConvertUsing(r => r.Name);
        }

        private void CreateSessionsMaps()
        {
            CreateMap<User, UserLoginInfoDto>();
            CreateMap<Tenant, TenantLoginInfoDto>();
        }
    }
}