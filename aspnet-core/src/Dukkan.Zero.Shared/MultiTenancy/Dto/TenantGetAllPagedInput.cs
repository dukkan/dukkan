using Abp.Application.Services.Dto;

namespace Dukkan.MultiTenancy.Dto
{
    public class TenantGetAllPagedInput : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}