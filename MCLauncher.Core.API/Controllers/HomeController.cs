using MCLauncher.DataAcess.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MCLauncher.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("GetServers")]
        public async void GetServers()
        {
            var ResponseString = string.Empty;
            var Servers = new List<Server>();

            using (var db = new MSLContext())
            {
                Servers = db.Servers.ToList();
            }

            ResponseString = JsonConvert.SerializeObject(Servers, Formatting.Indented);
            Response.ContentType = "application/json";

            await Response.Body.WriteAsync(Encoding.UTF8.GetBytes(ResponseString));
        }
    }
}
