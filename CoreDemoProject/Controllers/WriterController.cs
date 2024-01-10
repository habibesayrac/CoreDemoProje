using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CoreDemoProject.Models;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace CoreDemoProject.Controllers
{
	[AllowAnonymous]

	public class WriterController : Controller
    {

        WriterManager writerManager = new WriterManager(new EfWriterRepository());

        [Authorize]
        public IActionResult Index()
        {
            var usermail = User.Identity.Name;
            ViewBag.v = usermail;
            Context context = new();
            var writerName = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterName).FirstOrDefault();
            ViewBag.v2 = writerName;
            return View();
        }
        public IActionResult WriterProfile()
        {
            return View();
        }
        public IActionResult WriterMail()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Test()
        {
            return View();
        }

        [AllowAnonymous]

        public PartialViewResult WriterNavbarPartial()
        {
            return PartialView();
        }
        [AllowAnonymous]

        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }
      
        [HttpGet]
        public IActionResult WriterEditProfile()
        {
            Context context = new();
            var username = User.Identity.Name;
            var usermail = context.Users.Where(x=>x.UserName == username).Select(y=>y.Email).FirstOrDefault();
            var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var writervalues = writerManager.TGetById(writerID);
            return View(writervalues);
        }
     
        [HttpPost]
        public IActionResult WriterEditProfile(Writer p)
        {
            var pas1 = Request.Form["pass1"];
            var pas2 = Request.Form["pass2"];
            if (pas1 == pas2)
            {
                p.WriterPassword = pas2;
                WriterValidator validationRules = new WriterValidator();
                ValidationResult result = validationRules.Validate(p);
                if (result.IsValid)
                {
                    writerManager.TUpdate(p);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            else
            {
                ViewBag.hata = "Girdiğiniz Parolalar Uyuşmuyor!";
            }
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult WriterAdd()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult WriterAdd(AddProfileImage p)
        {
            Writer writer = new();
            if (p.WriterImage != null)
            {
                var extension = Path.GetExtension(p.WriterImage.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                p.WriterImage.CopyTo(stream);
                writer.WriterImage = newimagename;
            }
            writer.WriterMail = p.WriterMail;
            writer.WriterName = p.WriterName;
            writer.WriterPassword = p.WriterPassword;
            writer.WriterStatus = p.WriterStatus;
            writer.WriterAbout = p.WriterAbout;

            writerManager.TAdd(writer);
            return RedirectToAction("Index", "Dashboard");
        }
    }
}