using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemoProject.ViewComponents.Writer
{
	public class WriterAboutOnDashboard : ViewComponent
	{
		WriterManager writerManager = new WriterManager(new EfWriterRepository());

		Context context = new Context();

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var username = User.Identity.Name;
			ViewBag.veri = username;
			var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();

			var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
			var values = writerManager.GetWriterByID(writerID);
			return View(values);
		}
	}
}