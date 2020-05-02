using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Dukkan.Authorization.Roles.Dto;
using Dukkan.Authorization.Users.Dto;

namespace Dukkan.Authorization.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);

        Task<bool> ChangePassword(ChangePasswordDto input);
    }
}
