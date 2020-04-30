using Abp.Application.Services.Dto;

namespace Dukkan.Sessions.Dto
{
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
