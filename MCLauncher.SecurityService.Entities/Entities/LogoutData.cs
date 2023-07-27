using MCLauncher.SecurityService.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCLauncher.SecurityService.Entities
{
    public class LogoutData : ILogoutData
    {
        private string _username;

        public string Username { get => _username; set => _username = value; }
    }
}
