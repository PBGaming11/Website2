using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Controllers
{
    public class product_detail : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public product_detail(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index(int id)
        {
            SanPham sanpham = _db.sanpham.Include(s => s.danhmuc).FirstOrDefault(s => s.IdSanPham == id);
            if (sanpham == null)
            {
                return NotFound();
            }

            // Set ViewBag properties for the view
            ViewBag.SelectedCategory = sanpham.danhmuc;
            ViewBag.SelectedProduct = sanpham;

            return View(sanpham);
        }
    }
}
