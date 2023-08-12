using javax.jws;
using MCLauncher.SecurityService.Entities;
using MCLauncher.WebUI.Entities;
using MCLauncher.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

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
        public async Task<IActionResult> Home()
        {
            var HomeModel = new HomeViewModel();

            if (!Statics_WebUI.IsUserLogged())
                return RedirectToAction("Login", "Auth");

            ViewData.Add("ServerSelected", "Select A Server" + " ");
            ViewData.Add("ServerList", GetServers().Result);
            ViewData.Add("UserLogged", Statics_WebUI.UsernameLogged);

            return View("Home", HomeModel);
        }

        [Route("/SelectServer/{ServerName}")]
        public async Task<bool> SelectServer(string ServerName)
        {
            var servers = await GetServers();
            var server = servers.FirstOrDefault(f => f.ServerName == ServerName);

            return true;
        }

        public async Task<List<Server>> GetServers()
        {
            HttpClient client = new HttpClient();
            client.Timeout = new TimeSpan(0, 0, 10);
            HttpResponseMessage response;
            List<Server> servers = new List<Server>();

            response = await client.GetAsync("https://localhost:7003/api/Home/GetServers");

            if (response != null && response.IsSuccessStatusCode)
            {
                servers = JsonConvert.DeserializeObject<Server[]>(await response.Content.ReadAsStringAsync()).ToList();
            }

            return servers;
        }
    }
}