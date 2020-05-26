using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dukkan.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : DukkanControllerBase
    {
        public ActionResult Index() => View();
    }
}
