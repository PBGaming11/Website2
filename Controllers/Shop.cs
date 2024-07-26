using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;
using System.Linq;

namespace Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ShopController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int? categoryId, double? minPrice, double? maxPrice, string sortOrder, string searchQuery, int pageNumber = 1, int pageSize = 6)
        {
            var products = _db.sanpham.AsQueryable();
            DanhMuc selectedCategory = null;

            // Filter by category
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.DanhmucId == categoryId.Value);
                selectedCategory = _db.danhmuc.Find(categoryId.Value);
            }

            // Filter by price range
            if (minPrice.HasValue)
            {
                products = products.Where(p => p.GiaSanpham >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.GiaSanpham <= maxPrice.Value);
            }

            // Filter by search query
            if (!string.IsNullOrEmpty(searchQuery))
            {
                products = products.Where(p => p.TenSanPham.Contains(searchQuery));
            }

            // Sort products based on the selected order
            products = sortOrder switch
            {
                "asc" => products.OrderBy(p => p.GiaSanpham),
                "desc" => products.OrderByDescending(p => p.GiaSanpham),
                _ => products
            };

            // Calculate total pages
            int totalItems = products.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Get the products for the current page
            var paginatedProducts = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Pass data to the view
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.SortOrder = sortOrder;
            ViewBag.CategoryId = categoryId;
            ViewBag.SelectedCategory = selectedCategory;
            ViewBag.SearchQuery = searchQuery;

            return View(paginatedProducts);
        }
    }
}
