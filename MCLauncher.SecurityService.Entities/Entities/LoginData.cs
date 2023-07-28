using MCLauncher.SecurityService.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCLauncher.SecurityService.Entities
{
    public class LoginData : ILoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
