using Microsoft.AspNetCore.Mvc;

namespace CoreTiles.Server.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
