using Abp.Application.Services.Dto;

namespace Dukkan.Authorization.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

