using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Roles;
using Dukkan.Authorization.Users.Domain;

namespace Dukkan.Authorization.Roles.Domain
{
    public class Role : AbpRole<User>
    {
        public const int DescriptionMaxLength = 5000;

        public Role()
        {
        }

        public Role(int? tenantId, string displayName)
            : base(tenantId, displayName)
        {
        }

        public Role(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {
        }

        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; }
    }
}
