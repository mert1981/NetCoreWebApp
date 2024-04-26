﻿using Microsoft.AspNetCore.Mvc;
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
            if (ModelState.IsValid)  //modelde belirlediğimiz hatalar var mı kontrol ediyor.
            {
                _uygulamaDbContext.KitapTurleri.Add(kitapTuru);
                _uygulamaDbContext.SaveChanges();  //Save Changes yazmazsak bilgileri eklemez. 
                return RedirectToAction("Index", "KitapTuru"); //yazdıktan sonra bizi listeye atsın.
            }
            return View(); // Eğer modelde istenmeyen bir durum olursa viewe at
        }


        public IActionResult Guncelle(int? id)
        {
            if(id == null || id==0 )
                return NotFound(); 
            KitapTuru? kitapTuruVt = _uygulamaDbContext.KitapTurleri.Find(id);
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
                _uygulamaDbContext.KitapTurleri.Update(kitapTuru);
                _uygulamaDbContext.SaveChanges();  //Save Changes yazmazsak bilgileri eklemez. 
                return RedirectToAction("Index", "KitapTuru"); //yazdıktan sonra bizi listeye atsın.
            }
            return View(); // Eğer modelde istenmeyen bir durum olursa viewe at

        }
    }
}
