using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NetCoreWebApp.Models;
using NetCoreWebApp.Utility;

namespace NetCoreWebApp.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KitapController : Controller
    {
        private readonly IKitapRepository _kitapRepository;
        private readonly IKitapTuruRepository _kitapTuruRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public KitapController(IKitapRepository context, IKitapTuruRepository kitapTuruRepository, IWebHostEnvironment webHostEnvironment)
        {
            _kitapRepository = context;
            _kitapTuruRepository = kitapTuruRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Kitap> objKitapTuruList = _kitapRepository.GetAll(includeProps:"KitapTuru").ToList();
            
            return View(objKitapTuruList);
        }

        public IActionResult EkleGuncelle(int? id)
        {
            //Tüm Kitap Türlerini çekerek combo boxa atıyoruz
            IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll().Select(x => new SelectListItem
            { 
                Text = x.Ad,
                Value = x.Id.ToString()
            });
            ViewBag.KitapTuruList = KitapTuruList; //Controllerden View'e veri taşıma
            if (id==null || id==0)
            {
                //Ekle
                return View();
            }
            else
            {
                //Guncelle
                Kitap? kitapVt = _kitapRepository.Get(x => x.Id == id); //Gönderdiğimiz id'ye eşit olan kaydı getir.
                if (kitapVt == null)
                {
                    return NotFound();
                }
                return View(kitapVt);
            }
            
        }

        [HttpPost]
        public IActionResult EkleGuncelle(Kitap kitap,IFormFile? file) //IFormFile hazır bir sınıf dosyayı döndürüyor.
        {
            if (ModelState.IsValid)  //modelde belirlediğimiz hatalar var mı kontrol ediyor.
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath; //WWWroot 'un bulunduğu dizini verir.
                string kitapPath = Path.Combine(wwwRootPath, @"img"); // wwwroot/img 

                if (file != null) { //Guncelle yaptığımızda resmi güncellemek istemiyorsak null olarak geliyor. Null kontrol yapıyoruz.,
                    //EkleGuncelle viewda ise  <input asp-for="ResimUrl" hidden /> yazarak resimurl dolu olarak gidiyor.


                    using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    kitap.ResimUrl = @"\img\" + file.FileName;
                }
                

                if (kitap.Id == 0)
                {
                    _kitapRepository.Ekle(kitap);
                    TempData["basarili"] = "Kitap Başarıyla Eklendi";
                }
                else
                {
                    _kitapRepository.Guncelle(kitap);
                    TempData["basarili"] = "Kitap Başarıyla Guncellendi";
                }
                
                _kitapRepository.Kaydet();  //Save Changes yazmazsak bilgileri eklemez. 
                return RedirectToAction("Index", "Kitap"); //yazdıktan sonra bizi listeye atsın.
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
