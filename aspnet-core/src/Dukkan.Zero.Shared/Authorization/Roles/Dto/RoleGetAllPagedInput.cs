using Abp.Application.Services.Dto;

namespace Dukkan.Authorization.Roles.Dto
{
    public class RoleGetAllPagedInput : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

