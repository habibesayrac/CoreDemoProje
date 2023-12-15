using Microsoft.AspNetCore.Mvc;

namespace CoreDemoProject.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
