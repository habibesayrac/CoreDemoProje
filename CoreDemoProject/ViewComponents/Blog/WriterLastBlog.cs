using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemoProject.ViewComponents.Blog
{
	public class WriterLastBlog :ViewComponent
	{
		BlogManager blogManager = new BlogManager(new EfBlogRepository());
			
		public IViewComponentResult Invoke()
		{
			var values = blogManager.GetBlogByWriter(1);
			return View(values);
		}
	}
}