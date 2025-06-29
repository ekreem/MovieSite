using Microsoft.AspNetCore.Mvc;

namespace NetFlix.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
