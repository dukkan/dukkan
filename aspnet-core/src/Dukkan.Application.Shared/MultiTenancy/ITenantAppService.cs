using Abp.Application.Services;
using Dukkan.MultiTenancy.Dto;

namespace Dukkan.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

