using Microsoft.AspNetCore.Mvc;
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
            var sanpham = _db.sanpham.ToList();
            return View(sanpham);
        }
    }
}
