using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemoProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class AdminMessageController : Controller
    {
        Message2Manager message2Manager = new Message2Manager(new EfMessage2Repository());
        Context context = new Context();
        public IActionResult Inbox()
        {
            var username = User.Identity.Name;
            var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            //var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var writerID = 1;
            var values = message2Manager.GetInboxListByWriter(writerID);
            return View(values);
        }

        public IActionResult Sendbox()
        {
            Context context = new Context();
            var username = User.Identity.Name;
            var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = 1;

            var values = message2Manager.GetInboxListByWriter(writerID);
            return View(values);
        }
        [HttpGet]
        public IActionResult ComposeMessage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ComposeMessage(Message2 p)
        {
            var username = User.Identity.Name;
            var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            //var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var writerID = 1;
            p.SenderID = writerID;
            p.ReceiverID = 3;
            p.MessageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            p.MessageStatus = true;
            message2Manager.TAdd(p);
            return RedirectToAction("Sendbox");
        }
    }
}