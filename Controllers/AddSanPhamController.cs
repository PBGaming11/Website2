using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Controllers
{
    public class AddSanPhamController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AddSanPhamController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> HienSanPham(int page = 1, int pageSize = 10, double? minPrice = null, double? maxPrice = null, string sortOrder = "name")
        {
            var query = _db.sanpham.Include(sp => sp.danhmuc).AsQueryable();

            if (minPrice.HasValue)
            {
                // Ensure minPrice is compared with a double
                query = query.Where(sp => sp.GiaSanpham >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                // Ensure maxPrice is compared with a double
                query = query.Where(sp => sp.GiaSanpham <= maxPrice.Value);
            }

            switch (sortOrder)
            {
                case "price_desc":
                    query = query.OrderByDescending(sp => sp.GiaSanpham);
                    break;
                case "price_asc":
                    query = query.OrderBy(sp => sp.GiaSanpham);
                    break;
                default:
                    query = query.OrderBy(sp => sp.TenSanPham);
                    break;
            }

            int totalProducts = await query.CountAsync();
            var sanpham = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Use double for the total products and page size calculation
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.SortOrder = sortOrder;
            ViewBag.Sanpham = sanpham;

            return View();
        }


        [HttpGet]
        public IActionResult ThemSanpham()
        {
            ViewBag.DanhMucList = _db.danhmuc.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult ThemSanpham(SanPham sanpham)
        {
            sanpham.danhmuc = _db.danhmuc.FirstOrDefault(dm => dm.Id == sanpham.DanhmucId);
            if (ModelState.IsValid)
            {
                _db.Add(sanpham);
                _db.SaveChanges();
                return RedirectToAction(nameof(HienSanPham));
            }
            ViewBag.DanhMucList = _db.danhmuc.ToList();
            return View(sanpham);
        }

        [HttpGet]
        public IActionResult SuaSanpham(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var sanpham = _db.sanpham.Find(id);
            if (sanpham == null)
            {
                return NotFound();
            }
            ViewBag.DanhMucList = _db.danhmuc.ToList();
            return View(sanpham);
        }

        [HttpPost]
        public IActionResult SuaSanpham(SanPham sanpham)
        {
            sanpham.danhmuc = _db.danhmuc.FirstOrDefault(dm => dm.Id == sanpham.DanhmucId);
            if (!ModelState.IsValid)
            {
                _db.Update(sanpham);
                _db.SaveChanges();
                return RedirectToAction(nameof(HienSanPham));
            }
            ViewBag.DanhMucList = _db.danhmuc.ToList();
            return View(sanpham);
        }

        public IActionResult Delete(int id)
        {
            var sanpham = _db.sanpham.FirstOrDefault(sanpham => sanpham.IdSanPham == id);
            if (sanpham == null)
            {
                return NotFound();
            }
            else
            {
                _db.sanpham.Remove(sanpham);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(HienSanPham));
        }

    }
}
