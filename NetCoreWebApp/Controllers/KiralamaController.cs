using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NetCoreWebApp.Models;
using NetCoreWebApp.Utility;

namespace NetCoreWebApp.Controllers
{
    public class KiralamaController : Controller
    {
        private readonly IKitapRepository _kitapRepository;
        private readonly IKiralamaRepository _kiralamaRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public KiralamaController(IKitapRepository context, IKiralamaRepository kiralamaRepository, IWebHostEnvironment webHostEnvironment)
        {
            _kitapRepository = context;
            _kiralamaRepository = kiralamaRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Kiralama> objKiralamaList = _kiralamaRepository.GetAll(includeProps:"Kitap").ToList();
            
            return View(objKiralamaList);
        }

        public IActionResult EkleGuncelle(int? id)
        {
            //Tüm Kitap Türlerini çekerek combo boxa atıyoruz
            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll().Select(x => new SelectListItem
            { 
                Text = x.KitapAdi,
                Value = x.Id.ToString()
            });
            ViewBag.KitapList = KitapList; //Controllerden View'e veri taşıma
            if (id==null || id==0)
            {
                //Ekle
                return View();
            }
            else
            {
                //Guncelle
                Kiralama? kiralamaVt = _kiralamaRepository.Get(x => x.Id == id); //Gönderdiğimiz id'ye eşit olan kaydı getir.
                if (kiralamaVt == null)
                {
                    return NotFound();
                }
                return View(kiralamaVt);
            }
            
        }

        [HttpPost]
        public IActionResult EkleGuncelle(Kiralama kiralama) //IFormFile hazır bir sınıf dosyayı döndürüyor.
        {
            if (ModelState.IsValid)  //modelde belirlediğimiz hatalar var mı kontrol ediyor.
            {
            

                if (kiralama.Id == 0)
                {
                    _kiralamaRepository.Ekle(kiralama);
                    TempData["basarili"] = "Kiralama Başarıyla Eklendi";
                }
                else
                {
                    _kiralamaRepository.Guncelle(kiralama);
                    TempData["basarili"] = "Kiralama Başarıyla Guncellendi";
                }
                
                _kiralamaRepository.Kaydet();  //Save Changes yazmazsak bilgileri eklemez. 
                return RedirectToAction("Index", "Kiralama"); //yazdıktan sonra bizi listeye atsın.
            }
            return View(); // Eğer modelde istenmeyen bir durum olursa viewe at
        }

        /*
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
        */

        /*
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
        */


        public IActionResult Sil(int? id)
        {
            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll().Select(x => new SelectListItem
            {
                Text = x.KitapAdi,
                Value = x.Id.ToString()
            });
            ViewBag.KitapList = KitapList; //Controllerden View'e veri taşıma


            if (id == null || id == 0)
                return NotFound();
            Kiralama? kiralamaVt = _kiralamaRepository.Get(x => x.Id == id);
            if (kiralamaVt == null)
            {
                return NotFound();
            }
            return View(kiralamaVt);
        }

        [HttpPost,ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            Kiralama? kiralama = _kiralamaRepository.Get(x => x.Id == id);
            if (kiralama == null)
                return NotFound();
            _kiralamaRepository.Sil(kiralama);
            _kiralamaRepository.Kaydet();
            TempData["basarili"] = "Kiralama Başarıyla Silindi!";
            return RedirectToAction("Index", "Kiralama");
        }

    }
}
