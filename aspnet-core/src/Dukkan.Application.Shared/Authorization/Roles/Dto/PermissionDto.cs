using Abp.Application.Services.Dto;

namespace Dukkan.Roles.Dto
{
    public class PermissionDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }
    }
}
