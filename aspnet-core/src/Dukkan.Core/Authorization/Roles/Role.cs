using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Roles;
using Dukkan.Authorization.Users;

namespace Dukkan.Authorization.Roles
{
    public class Role : AbpRole<User>
    {
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

        [StringLength(RoleConsts.DescriptionMaxLength)]
        public string Description { get; set; }
    }
}
