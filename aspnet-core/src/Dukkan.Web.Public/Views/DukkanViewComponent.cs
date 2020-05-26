using Abp.AspNetCore.Mvc.ViewComponents;

namespace Dukkan.Web.Views
{
    public abstract class DukkanViewComponent : AbpViewComponent
    {
        protected DukkanViewComponent()
        {
            LocalizationSourceName = DukkanConsts.LocalizationSourceName;
        }
    }
}
