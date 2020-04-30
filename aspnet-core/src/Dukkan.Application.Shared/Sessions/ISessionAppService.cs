using System.Threading.Tasks;
using Abp.Application.Services;
using Dukkan.Sessions.Dto;

namespace Dukkan.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
