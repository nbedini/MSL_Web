using MCLauncher.WebUI.Entities;
using MCLauncher.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MCLauncher.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("/Home")]
        public IActionResult Home()
        {
            if (!Statics_WebUI.IsUserLogged && string.IsNullOrEmpty(Statics_WebUI.UsernameLogged))
                return RedirectToAction("Login", "Auth");

            return View("Home");
        }
    }
}