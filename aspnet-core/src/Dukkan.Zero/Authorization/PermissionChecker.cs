using Abp.Authorization;
using Dukkan.Authorization.Roles.Domain;
using Dukkan.Authorization.Users.Domain;

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
