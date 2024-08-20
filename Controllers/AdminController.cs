using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
