using MCLauncher.SecurityService.DataAccess.Model;
using MCLauncher.SecurityService.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.Xml;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MCLauncher.SecurityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<LoginResult> Login()
        {
            User? user = null;
            LoginResult result = new() { LoginStatus = LoginErrorStatus.Success, ErrorText = string.Empty };
            var loginData = JsonConvert.DeserializeObject<LoginData>(await (new StreamReader(Request.Body)).ReadToEndAsync());

            using (var db = new SecurityDbContext())
            {
                user = db.Users.FirstOrDefault(f => f.Username.ToLower() == loginData.Username.ToLower() && f.Password == loginData.Password);

                // Evaluate user not found
                if (user == null)
                {
                    result.ErrorText = "User not found, try again!";
                    result.LoginStatus = LoginErrorStatus.UserNotFound;
                    return result;
                }
                else if (Convert.ToBoolean(user.IsLogged) && !loginData.ForceLoginRequired)
                {
                    result.ErrorText = "User already logged, Impossible to complete operation.";
                    result.LoginStatus = LoginErrorStatus.UserAlreadyLogged;
                    return result;
                }
                else if (loginData.ForceLoginRequired)
                {
                    var updateUser = user;
                    updateUser.LastLoginDateTime = DateTime.Now;

                    db.Users.Update(updateUser);
                    db.SaveChanges();

                    return result;
                }
                else
                {
                    var updateUser = user;
                    updateUser.LastLoginDateTime = DateTime.Now;
                    updateUser.IsLogged = true;

                    db.Users.Update(updateUser);
                    db.SaveChanges();
                
                    return result;
                }
            }
        }

        [HttpPost("Logout")]
        public async Task<LogoutResult> Logout()
        {
            User? user;
            LogoutResult result = new() { LogoutStatus = LogoutErrorStatus.Success, ErrorText = string.Empty };
            var logoutData = JsonConvert.DeserializeObject<LogoutData>(await(new StreamReader(Request.Body)).ReadToEndAsync());

            using (var db = new SecurityDbContext())
            {
                user = db.Users.FirstOrDefault(f => f.Username.ToLower() == logoutData.Username.ToLower());

                if (user == null)
                {
                    result.LogoutStatus = LogoutErrorStatus.UserNotFound;
                    result.ErrorText = "User not found, try again!";
                    return result;
                }
                else if (!Convert.ToBoolean(user.IsLogged))
                {
                    result.LogoutStatus = LogoutErrorStatus.UserNotLogged;
                    result.ErrorText = "User not logged, impossiblem to complete operations!";
                    return result;
                }
                else
                {
                    var updateUser = user;
                    updateUser.LastLogoutDateTime = DateTime.Now;
                    updateUser.IsLogged = false;

                    db.Users.Update(updateUser);
                    db.SaveChanges();

                    return result;
                }
            }
        }
    }
}
