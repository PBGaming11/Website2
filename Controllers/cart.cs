using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class cart : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
