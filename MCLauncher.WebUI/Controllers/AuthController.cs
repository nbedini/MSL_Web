using MCLauncher.SecurityService.Entities;
using MCLauncher.WebUI.Entities;
using MCLauncher.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
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

        [Route("Login/Error")]
        public IActionResult GetLoginErrorView(LoginViewModel model)
        {
            return View("LoginPage", model);
        }

        public async Task<IActionResult> DoLogin()
        {
            var model = await HttpContext.Request.ReadFormAsync();
            LoginViewModel LoginData = new LoginViewModel();
            HttpClient client = new HttpClient();
            client.Timeout = new TimeSpan(0, 0, 10);
            StringContent content;
            HttpResponseMessage response;

            if (ModelState.IsValid)
            {
                LoginData.Username = model["Username"].ToString();
                LoginData.Password = model["Password"].ToString();
                LoginData.ForceLoginRequired = model["ForceLoginRequired"].FirstOrDefault() != null ? Convert.ToBoolean(model["ForceLoginRequired"].First().ToString()) : false;

                if (!LoginData.ForceLoginRequired)
                {
                    content = new StringContent(JsonConvert.SerializeObject(LoginData), Encoding.UTF8, "application/json");
                    response = await client.PostAsync("https://localhost:7294/api/Authentication/Login", content);
                }
                else
                {
                    content = new StringContent(JsonConvert.SerializeObject(LoginData), Encoding.UTF8, "application/json");
                    response = await client.PostAsync("https://localhost:7294/api/Authentication/Login", content);
                }

                LoginResult loginResult;

                if (response.IsSuccessStatusCode)
                {
                    loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();

                    if (loginResult != null && loginResult.LoginStatus == LoginErrorStatus.Success && string.IsNullOrEmpty(loginResult.ErrorText))
                    {
                        Statics_WebUI.UsernameLogged = LoginData.Username;
                        Statics_WebUI.UserLogged = true;

                        return RedirectToAction("Home", "Home");
                    }
                    else if (loginResult.LoginStatus == LoginErrorStatus.UserAlreadyLogged)
                    {
                        var RedirectModel = new LoginViewModel();
                        RedirectModel.UserAlreadyLogged = true;

                        return RedirectToAction("GetLoginErrorView", "Auth", RedirectModel);
                    }
                }
            }

            return Ok();
        }

        [HttpGet("Logout/{username}")]
        public async Task<bool> DoLogout(string username)
        {
            LogoutViewModel LogoutData = new LogoutViewModel();
            HttpClient client = new HttpClient();
            client.Timeout = new TimeSpan(0, 0, 10);
            StringContent content;
            HttpResponseMessage response;

            if (ModelState.IsValid)
            {
                LogoutData.Username = username;

                content = new StringContent(JsonConvert.SerializeObject(LogoutData), Encoding.UTF8, "application/json");
                response = await client.PostAsync("https://localhost:7294/api/Authentication/Logout", content);

                LoginResult loginResult;

                if (response.IsSuccessStatusCode)
                {
                    Statics_WebUI.UserLogged = false;
                    Statics_WebUI.UsernameLogged = string.Empty;

                    return true;
                }
            }

            return false;
        }
    }
}