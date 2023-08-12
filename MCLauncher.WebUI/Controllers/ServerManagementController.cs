using MCLauncher.WebUI.Entities;
using MCLauncher.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MCLauncher.WebUI.Controllers
{
    public class ServerManagementController : Controller
    {
        [HttpGet("/ServerManagement/{ServerName}")]
        public async Task<IActionResult> Home(string ServerName)
        {
            ServerManagementViewModel model = new ServerManagementViewModel();

            if(!Statics_WebUI.UserLogged && Statics_WebUI.UsernameLogged == string.Empty)
                return RedirectToAction("Home", "Home");

            ViewData.Add("ServerSelected", ServerName);
            ViewData.Add("UserLogged", Statics_WebUI.UsernameLogged);
            //model = await GetServer(ServerName);

            return View("Home", model);
        }

        public async Task<Server> GetServer(string ServerName)
        {
            HttpClient client = new HttpClient();
            client.Timeout = new TimeSpan(0, 0, 10);
            HttpResponseMessage response;
            Server server = new Server();

            var content = new StringContent(JsonConvert.SerializeObject(ServerName));
            response = await client.PostAsync("https://localhost:7003/api/Home/GetServer", content);

            if (response != null && response.IsSuccessStatusCode)
            {
                server = JsonConvert.DeserializeObject<Server>(await response.Content.ReadAsStringAsync());
            }

            return server;
        }
    }
}
