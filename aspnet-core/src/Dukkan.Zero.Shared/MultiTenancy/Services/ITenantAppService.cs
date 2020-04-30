using Abp.Application.Services;
using Dukkan.MultiTenancy.Dto;

namespace Dukkan.MultiTenancy.Services
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, TenantGetAllPagedInput, TenantCreateDto, TenantDto>
    {
    }
}