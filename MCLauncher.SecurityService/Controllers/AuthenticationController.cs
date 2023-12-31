﻿using MCLauncher.SecurityService.DataAccess.Model;
using MCLauncher.SecurityService.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MCLauncher.SecurityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthenticationController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public LoginResult Login([FromBody] LoginData loginData)
        {
            User? user;
            LoginResult result = new() { LoginStatus = LoginErrorStatus.Success, ErrorText = string.Empty };

            using (var db = new SecurityDbContext())
            {
                user = db.Users.FirstOrDefault(f => f.Username.ToLower() == loginData.Username.ToLower() && f.Password == loginData.Password);
            }

            // Evaluate user not found
            if (user == null)
            {
                result.ErrorText = "User not found, try again!";
                result.LoginStatus = LoginErrorStatus.UserNotFound;
                return result;
            }
            else if (Convert.ToBoolean(user.IsLogged))
            {
                result.ErrorText = "User already logged, Impossible to complete operation.";
                result.LoginStatus = LoginErrorStatus.UserAlreadyLogged;
                return result;
            }
            else
            {
                // Set db field
                using (var db = new SecurityDbContext())
                {
                    var updateUser = user;
                    updateUser.LastLoginDateTime = DateTime.Now;
                    updateUser.IsLogged = true;

                    db.Users.Update(updateUser);
                    db.SaveChanges();
                }

                return result;
            }
        }

        [HttpPost("Logout")]
        public LogoutResult Logout([FromBody] LogoutData logoutData)
        {
            User? user;
            LogoutResult result = new() { LogoutStatus = LogoutErrorStatus.Success, ErrorText = string.Empty };

            using (var db = new SecurityDbContext())
            {
                user = db.Users.FirstOrDefault(f => f.Username.ToLower() == logoutData.Username.ToLower());
            }

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
                // Set db field
                using (var db = new SecurityDbContext())
                {
                    var updateUser = user;
                    updateUser.LastLogoutDateTime = DateTime.Now;
                    updateUser.IsLogged = false;

                    db.Users.Update(updateUser);
                    db.SaveChanges();
                }

                return result;
            }
        }
    }
}
