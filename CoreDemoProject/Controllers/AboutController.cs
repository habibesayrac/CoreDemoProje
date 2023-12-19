using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemoProject.Controllers
{
	public class AboutController : Controller
	{
		AboutManager aboutManager = new AboutManager(new EfAboutRepository());
		public IActionResult Index()
		{
			return View();
		}
		public PartialViewResult SocialMediaAbout()
		{
			var values = aboutManager.GetList();
			return PartialView(values);
		}
	}
}