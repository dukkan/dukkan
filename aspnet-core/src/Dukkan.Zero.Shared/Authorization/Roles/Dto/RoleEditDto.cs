using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Roles;
using Dukkan.Authorization.Roles.Domain;

namespace Dukkan.Authorization.Roles.Dto
{
    public class RoleEditDto: EntityDto<int>
    {
        [Required]
        [StringLength(AbpRoleBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpRoleBase.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        [StringLength(Role.DescriptionMaxLength)]
        public string Description { get; set; }

        public bool IsStatic { get; set; }
    }
}