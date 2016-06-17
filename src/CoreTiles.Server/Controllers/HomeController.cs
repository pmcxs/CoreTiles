using Microsoft.AspNetCore.Mvc;
using System;

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
