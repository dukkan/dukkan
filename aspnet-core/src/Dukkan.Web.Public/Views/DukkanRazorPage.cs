using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Dukkan.Web.Views
{
    public abstract class DukkanRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected DukkanRazorPage()
        {
            LocalizationSourceName = DukkanConsts.LocalizationSourceName;
        }
    }
}
