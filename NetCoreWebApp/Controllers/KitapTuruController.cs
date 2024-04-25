using Microsoft.AspNetCore.Mvc;

namespace NetCoreWebApp.Controllers
{
    public class KitapTuruController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
