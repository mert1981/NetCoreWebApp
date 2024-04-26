using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NetCoreWebApp.Models;
using NetCoreWebApp.Utility;

namespace NetCoreWebApp.Controllers
{
    public class KitapTuruController : Controller
    {
        private readonly IKitapTuruRepository _kitapTuruRepository;
        public KitapTuruController(IKitapTuruRepository context)
        {
            _kitapTuruRepository = context;
        }
        public IActionResult Index()
        {
            List<KitapTuru> objKitapTuruList = _kitapTuruRepository.GetAll().ToList();
            return View(objKitapTuruList);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(KitapTuru kitapTuru)
        {
            if (ModelState.IsValid)  //modelde belirlediğimiz hatalar var mı kontrol ediyor.
            {
                _kitapTuruRepository.Ekle(kitapTuru);
                _kitapTuruRepository.Kaydet();  //Save Changes yazmazsak bilgileri eklemez. 
                TempData["basarili"] = "Kitap Başarıyla Eklendi";
                return RedirectToAction("Index", "KitapTuru"); //yazdıktan sonra bizi listeye atsın.
            }
            return View(); // Eğer modelde istenmeyen bir durum olursa viewe at
        }


        public IActionResult Guncelle(int? id)
        {
            if(id == null || id==0 )
                return NotFound();
            KitapTuru? kitapTuruVt = _kitapTuruRepository.Get(x=>x.Id==id); //Gönderdiğimiz id'ye eşit olan kaydı getir.
            if (kitapTuruVt == null)
            {
                return NotFound();
            }
            return View(kitapTuruVt);
        }

        [HttpPost]
        public IActionResult Guncelle(KitapTuru kitapTuru)
        {
            if (ModelState.IsValid)  //modelde belirlediğimiz hatalar var mı kontrol ediyor.
            {
                _kitapTuruRepository.Guncelle(kitapTuru);
                _kitapTuruRepository.Kaydet();  //Save Changes yazmazsak bilgileri eklemez. 
                TempData["basarili"] = "Kitap Başarıyla Güncellendi!";
                return RedirectToAction("Index", "KitapTuru"); //yazdıktan sonra bizi listeye atsın.
            }
            return View(); // Eğer modelde istenmeyen bir durum olursa viewe at
        }


        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            KitapTuru? kitapTuruVt = _kitapTuruRepository.Get(x => x.Id == id);
            if (kitapTuruVt == null)
            {
                return NotFound();
            }
            return View(kitapTuruVt);
        }

        [HttpPost,ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            KitapTuru? kitapTuru = _kitapTuruRepository.Get(x => x.Id == id);
            if (kitapTuru == null)
                return NotFound();
            _kitapTuruRepository.Sil(kitapTuru);
            _kitapTuruRepository.Kaydet();
            TempData["basarili"] = "Kitap Başarıyla Silindi!";
            return RedirectToAction("Index", "KitapTuru");
        }

    }
}
