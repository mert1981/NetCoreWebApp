using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NetCoreWebApp.Models;
using NetCoreWebApp.Utility;

namespace NetCoreWebApp.Controllers
{
    public class KitapController : Controller
    {
        private readonly IKitapRepository _kitapRepository;
        private readonly IKitapTuruRepository _kitapTuruRepository;
        public KitapController(IKitapRepository context, IKitapTuruRepository kitapTuruRepository)
        {
            _kitapRepository = context;
            _kitapTuruRepository = kitapTuruRepository;
        }
        public IActionResult Index()
        {
            List<Kitap> objKitapTuruList = _kitapRepository.GetAll().ToList();
            
            return View(objKitapTuruList);
        }

        public IActionResult Ekle()
        {
            //Tüm Kitap Türlerini çekerek combo boxa atıyoruz
            IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll().Select(x => new SelectListItem
            { 
                Text = x.Ad,
                Value = x.Id.ToString()
            });
            ViewBag.KitapTuruList = KitapTuruList; //Controllerden View'e veri taşıma
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Kitap kitap)
        {
            if (ModelState.IsValid)  //modelde belirlediğimiz hatalar var mı kontrol ediyor.
            {
                _kitapRepository.Ekle(kitap);
                _kitapRepository.Kaydet();  //Save Changes yazmazsak bilgileri eklemez. 
                TempData["basarili"] = "Kitap Başarıyla Eklendi";
                return RedirectToAction("Index", "Kitap"); //yazdıktan sonra bizi listeye atsın.
            }
            return View(); // Eğer modelde istenmeyen bir durum olursa viewe at
        }


        public IActionResult Guncelle(int? id)
        {
            if(id == null || id==0 )
                return NotFound();
            Kitap? kitapVt = _kitapRepository.Get(x=>x.Id==id); //Gönderdiğimiz id'ye eşit olan kaydı getir.
            if (kitapVt == null)
            {
                return NotFound();
            }
            return View(kitapVt);
        }

        [HttpPost]
        public IActionResult Guncelle(Kitap kitap)
        {
            if (ModelState.IsValid)  //modelde belirlediğimiz hatalar var mı kontrol ediyor.
            {
                _kitapRepository.Guncelle(kitap);
                _kitapRepository.Kaydet();  //Save Changes yazmazsak bilgileri eklemez. 
                TempData["basarili"] = "Kitap Başarıyla Güncellendi!";
                return RedirectToAction("Index", "Kitap"); //yazdıktan sonra bizi listeye atsın.
            }
            return View(); // Eğer modelde istenmeyen bir durum olursa viewe at
        }


        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            Kitap? kitapVt = _kitapRepository.Get(x => x.Id == id);
            if (kitapVt == null)
            {
                return NotFound();
            }
            return View(kitapVt);
        }

        [HttpPost,ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            Kitap? kitap = _kitapRepository.Get(x => x.Id == id);
            if (kitap == null)
                return NotFound();
            _kitapRepository.Sil(kitap);
            _kitapRepository.Kaydet();
            TempData["basarili"] = "Kitap Başarıyla Silindi!";
            return RedirectToAction("Index", "Kitap");
        }

    }
}
