﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemoProject.Areas.Admin.Controllers
{
	[AllowAnonymous]
	public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
