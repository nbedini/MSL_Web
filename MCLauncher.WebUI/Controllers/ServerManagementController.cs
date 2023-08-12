using Microsoft.AspNetCore.Mvc;

namespace MCLauncher.WebUI.Controllers
{
    public class ServerManagementController : Controller
    {
        [HttpGet("/ServerManagement/{ServerName}")]
        public IActionResult Home(string ServerName)
        {
            return View("Home");
        }
    }
}
