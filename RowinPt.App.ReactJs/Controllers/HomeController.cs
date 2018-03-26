using Microsoft.AspNetCore.Mvc;

namespace RowinPt.App.ReactJs.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
