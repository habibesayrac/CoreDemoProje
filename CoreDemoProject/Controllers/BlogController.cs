﻿using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreDemoProject.Controllers
{
	[AllowAnonymous]
	public class BlogController : Controller
	{
		BlogManager blogManager = new BlogManager(new EfBlogRepository());
		CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
		Context context = new();

		public IActionResult Index()
		{
			var values = blogManager.GetBlogListWithCategory();
			return View(values);
		}
		public IActionResult BlogReadAll(int id)
		{
			ViewBag.i = id;
			var values = blogManager.GetBlogByID(id);
			return View(values);

		}
		public IActionResult BlogListByWriter()
		{
			var username = User.Identity.Name;
			var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
			var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
			var values = blogManager.GetListWithCategoryByWriterBm(writerID);
			return View(values);
		}

		[HttpGet]
		public IActionResult BlogAdd()
		{
			List<SelectListItem> categoryvalues = (from x in categoryManager.GetList()
												   select new SelectListItem
												   {
													   Text = x.CategoryName,
													   Value = x.CategoryID.ToString()
												   }).ToList();
			ViewBag.cv = categoryvalues;
			return View();
		}

		[HttpPost]
		public IActionResult BlogAdd(Blog p)
		{
			var username = User.Identity.Name;
			var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
			var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();

			BlogValidator bv = new BlogValidator();

			ValidationResult results = bv.Validate(p);
			if (results.IsValid)
			{
				p.BlogStatus = true;
				p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
				p.WriterID = writerID;

				blogManager.TAdd(p);

				return RedirectToAction("BlogListByWriter", "Blog");
			}
			else
			{
				foreach (var item in results.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
			}
			return View();
		}
		public IActionResult DeleteBlog(int id)
		{
			var blogvalue = blogManager.TGetById(id);
			blogManager.TDelete(blogvalue);
			return RedirectToAction("BlogListByWriter");
		}
		[HttpGet]
		public IActionResult EditBlog(int id)
		{
			var blogvalue = blogManager.TGetById(id);
			List<SelectListItem> categoryvalues = (from x in categoryManager.GetList()
												   select new SelectListItem
												   {
													   Text = x.CategoryName,
													   Value = x.CategoryID.ToString()
												   }).ToList();
			ViewBag.cv = categoryvalues;

			return View(blogvalue);
		}
		[HttpPost]
		public IActionResult EditBlog(Blog p)
		{
			var username = User.Identity.Name;
			var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
			var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
			p.WriterID = writerID;
			p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
			p.BlogStatus = true;
			blogManager.TUpdate(p);
			return RedirectToAction("BlogListByWriter");
		}
	}
}