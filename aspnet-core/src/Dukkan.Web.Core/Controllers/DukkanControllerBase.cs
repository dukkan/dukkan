using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Dukkan.Web.Controllers
{
    public abstract class DukkanControllerBase: AbpController
    {
        protected DukkanControllerBase()
        {
            LocalizationSourceName = DukkanConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
