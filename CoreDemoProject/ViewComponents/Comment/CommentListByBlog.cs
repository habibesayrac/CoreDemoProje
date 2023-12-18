using BusinessLayer.Concrete;
using CoreDemoProject.Models;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemoProject.ViewComponents.Comment
{
	public class CommentListByBlog : ViewComponent
	{
		CommentManager commentManager = new CommentManager(new EfCommentRepository());
		public IViewComponentResult Invoke(int id)
		{
			var values = commentManager.GetList(id);

			return View(values);
		}
	}
}