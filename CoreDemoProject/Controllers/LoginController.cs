using Microsoft.AspNetCore.Mvc;

namespace CoreDemoProject.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
