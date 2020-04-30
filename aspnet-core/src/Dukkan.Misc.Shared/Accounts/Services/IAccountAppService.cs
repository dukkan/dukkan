using System.Threading.Tasks;
using Abp.Application.Services;
using Dukkan.Accounts.Dto;

namespace Dukkan.Accounts.Services
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
