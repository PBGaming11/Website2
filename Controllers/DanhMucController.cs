using Microsoft.AspNetCore.Mvc;
using Website.Data;
using Website.Models;

namespace Website.Controllers
{
    public class DanhMucController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DanhMucController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var danhmuc = _db.danhmuc.ToList();
            ViewBag.danhmuc = danhmuc;
            return View();
        }
        [HttpGet]
        public IActionResult ThemDanhMuc()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ThemDanhMuc(DanhMuc danhmuc)
        {
            if (!ModelState.IsValid)
            {
                danhmuc.Description = DateTime.Now;
                _db.Add(danhmuc);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult SuaDanhMuc(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var danhmuc = _db.danhmuc.Find(id);
            return View(danhmuc);
        }
        [HttpPost]
        public IActionResult SuaDanhMuc(DanhMuc danhmuc)
        {
            if (ModelState.IsValid)
            {
                danhmuc.Description = DateTime.Now;
                _db.Update(danhmuc);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult XoaDanhMuc(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var danhmuc = _db.danhmuc.Find(id);
            return View(danhmuc);
        }
        [HttpPost]
        public IActionResult XacNhanXoa(int id)
        {
            var danhmuc = _db.danhmuc.Find(id);
            if (danhmuc == null)
            {
                return NotFound();
            }
            _db.danhmuc.Remove(danhmuc);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
