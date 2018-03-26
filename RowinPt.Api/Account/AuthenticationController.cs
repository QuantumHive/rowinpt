using Microsoft.AspNetCore.Mvc;

namespace RowinPt.Api.Account
{
    public class AuthenticationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
