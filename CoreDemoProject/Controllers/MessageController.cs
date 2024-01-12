using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreDemoProject.Controllers
{
    [AllowAnonymous]
    public class MessageController : Controller
    {
        Message2Manager message2Manager = new Message2Manager(new EfMessage2Repository());
        public IActionResult InBox()
        {
            Context context = new Context();
			var username = User.Identity.Name;
			var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
			var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
		
            var values = message2Manager.GetInboxListByWriter(writerID);
            return View(values);
        }
        public IActionResult Sendbox()
        {
            Context context = new Context();
			var username = User.Identity.Name;
			var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
			var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
		
            var values = message2Manager.GetInboxListByWriter(writerID);
            return View(values);
        }

        public IActionResult MessageDetails(int id)
        {
            var value = message2Manager.TGetById(id);

            return View(value);
        }
    }
}