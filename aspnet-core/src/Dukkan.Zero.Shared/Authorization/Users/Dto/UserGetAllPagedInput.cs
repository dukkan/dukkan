using Abp.Application.Services.Dto;

namespace Dukkan.Authorization.Users.Dto
{
    public class UserGetAllPagedInput : PagedResultRequestDto
    {
        public string Keyword { get; set; }


        public bool? IsActive { get; set; }
    }
}