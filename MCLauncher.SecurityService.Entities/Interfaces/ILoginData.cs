using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCLauncher.SecurityService.Entities.Interfaces
{
    internal interface ILoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
