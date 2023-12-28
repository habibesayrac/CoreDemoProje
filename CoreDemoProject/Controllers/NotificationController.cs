using Microsoft.AspNetCore.Mvc;

namespace CoreDemoProject.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
