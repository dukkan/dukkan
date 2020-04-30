using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Dukkan.Authorization.Roles.Dto;

namespace Dukkan.Authorization.Roles.Services
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, RoleGetAllPagedInput, RoleCreateDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();

        Task<RoleGetForEditOutput> GetRoleForEdit(EntityDto input);

        Task<ListResultDto<RoleListDto>> GetRolesAsync(RoleGetAllInput input);
    }
}
