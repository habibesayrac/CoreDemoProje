using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemoProject.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AllowAnonymous]
	public class AdminMessageController : Controller
	{
		public IActionResult Inbox()
		{
			return View();
		}
	}
}
