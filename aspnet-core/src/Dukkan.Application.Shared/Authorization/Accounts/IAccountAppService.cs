using System.Threading.Tasks;
using Abp.Application.Services;
using Dukkan.Authorization.Accounts.Dto;

namespace Dukkan.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
