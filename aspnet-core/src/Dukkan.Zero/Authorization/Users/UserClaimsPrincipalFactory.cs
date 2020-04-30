using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using Dukkan.Authorization.Roles.Domain;
using Dukkan.Authorization.Users.Domain;

namespace Dukkan.Authorization.Users
{
    public class UserClaimsPrincipalFactory : AbpUserClaimsPrincipalFactory<User, Role>
    {
        public UserClaimsPrincipalFactory(
            UserManager userManager,
            RoleManager roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(
                  userManager,
                  roleManager,
                  optionsAccessor)
        {
        }
    }
}
