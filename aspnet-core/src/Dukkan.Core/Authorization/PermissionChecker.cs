using Abp.Authorization;
using Dukkan.Authorization.Roles;
using Dukkan.Authorization.Users;

namespace Dukkan.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
