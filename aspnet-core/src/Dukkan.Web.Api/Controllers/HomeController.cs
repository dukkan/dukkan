using Microsoft.AspNetCore.Mvc;

namespace Dukkan.Web.Controllers
{
    public class HomeController : DukkanControllerBase
    {
        public IActionResult Index() => Redirect("/swagger");
    }
}
