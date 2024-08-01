using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website.Data;

namespace Website.ViewCompoments
{
    public class NewestProductCompoment :ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public NewestProductCompoment(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var products = _db.sanpham.Include(p => p.danhmuc).ToList();

            // Select 6 random products
            var randomProducts = products.OrderBy(p => Guid.NewGuid()).Take(6);

            return View(randomProducts);
        }
    }
}
