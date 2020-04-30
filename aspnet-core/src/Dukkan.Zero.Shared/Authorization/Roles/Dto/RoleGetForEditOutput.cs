using System.Collections.Generic;

namespace Dukkan.Authorization.Roles.Dto
{
    public class RoleGetForEditOutput
    {
        public RoleEditDto Role { get; set; }

        public List<PermissionFlatDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}