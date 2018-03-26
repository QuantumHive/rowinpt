using Microsoft.AspNetCore.Mvc;

namespace RowinPt.Management.ReactJs.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
