using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Dukkan.Authorization.Roles.Dto;
using Dukkan.Authorization.Users.Dto;

namespace Dukkan.Authorization.Users.Services
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, UserGetAllPagedInput, UserCreateDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(UserChangeLanguageDto input);

        Task<bool> ChangePassword(UserChangePasswordDto input);
    }
}