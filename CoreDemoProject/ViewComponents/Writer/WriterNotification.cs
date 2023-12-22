using Microsoft.AspNetCore.Mvc;

namespace CoreDemoProject.ViewComponents.Writer
{
    public class WriterNotification:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
