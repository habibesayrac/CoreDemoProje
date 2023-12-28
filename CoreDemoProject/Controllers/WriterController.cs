using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CoreDemoProject.Models;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace CoreDemoProject.Controllers
{
    public class WriterController : Controller
    {
        WriterManager writerManager = new WriterManager(new EfWriterRepository());
        public IActionResult Index()
        {
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
        [AllowAnonymous]
        [HttpGet]
        public IActionResult WriterEditProfile()
        {
            var writervalues = writerManager.TGetById(1);
            return View(writervalues);
        }
        [AllowAnonymous]
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
            if (p.WriterImage!=null)
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