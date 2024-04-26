using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NetCoreWebApp.Models;
using NetCoreWebApp.Utility;

namespace NetCoreWebApp.Controllers
{
    public class KitapTuruController : Controller
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        public KitapTuruController(UygulamaDbContext context)
        {
            _uygulamaDbContext = context;
        }
        public IActionResult Index()
        {
            List<KitapTuru> objKitapTuruList = _uygulamaDbContext.KitapTurleri.ToList();
            return View(objKitapTuruList);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(KitapTuru kitapTuru)
        {
            _uygulamaDbContext.KitapTurleri.Add(kitapTuru);
            _uygulamaDbContext.SaveChanges();  //Save Changes yazmazsak bilgileri eklemez. 
            return RedirectToAction("Index","KitapTuru"); //yazdıktan sonra bizi listeye atsın.
        }
    }
}
