using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemoProject.ViewComponents.Writer
{
    public class WriterMessageNotification:ViewComponent
    {
        WriterManager writerManager = new WriterManager(new EfWriterRepository());
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
