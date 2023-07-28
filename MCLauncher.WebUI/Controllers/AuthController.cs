using MCLauncher.SecurityService.Entities;
using MCLauncher.WebUI.Entities;
using MCLauncher.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Diagnostics;
using System.Text;

namespace MCLauncher.WebUI.Controllers
{
    [Route("Auth")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [Route("Login")]
        public IActionResult GetLoginView()
        {
            return View("LoginPage");
        }

        public async Task<IActionResult> DoLogin()
        {
            var model = await HttpContext.Request.ReadFormAsync();
            LoginViewModel LoginData = new LoginViewModel();
            HttpClient client = new HttpClient();
            client.Timeout = new TimeSpan(0, 0, 10);
            StringContent content;

            var pippo = this.Request.Host.ToUriComponent();

            if (ModelState.IsValid)
            {
                LoginData.Username = model["Username"].ToString();
                LoginData.Password = model["Password"].ToString();

                content = new StringContent(JsonConvert.SerializeObject(LoginData), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:7294/api/Authentication/Login", content);

                LoginResult loginResult;

                if (response.IsSuccessStatusCode)
                {
                    loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();

                    if (loginResult != null && loginResult.LoginStatus == LoginErrorStatus.Success && string.IsNullOrEmpty(loginResult.ErrorText))
                    {
                        Statics_WebUI.UsernameLogged = LoginData.Username;
                        Statics_WebUI.IsUserLogged = true;

                        return Redirect($"https://{this.Request.Host.ToUriComponent()}/Home");
                    }
                }
            }

            return Ok();
        }
    }
}