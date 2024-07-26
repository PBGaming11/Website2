using Microsoft.AspNetCore.Mvc;
using Website.Data;
namespace Website.ViewCompoments

{
    public class NavCompoments : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public NavCompoments(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var danhmuc = _db.danhmuc.ToList();
            return View(danhmuc);
        }
    }
}
