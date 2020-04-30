using System.Threading.Tasks;
using Abp.Application.Services;
using Dukkan.Sessions.Dto;

namespace Dukkan.Sessions.Services
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
