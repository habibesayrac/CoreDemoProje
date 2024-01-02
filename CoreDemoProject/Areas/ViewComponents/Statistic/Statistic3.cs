using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemoProject.Areas.ViewComponents.Statistic
{
    public class Statistic3:ViewComponent
    {
        Context c = new Context();
        public IViewComponentResult Invoke()
        {
           
            return View();
        }
    }
}
