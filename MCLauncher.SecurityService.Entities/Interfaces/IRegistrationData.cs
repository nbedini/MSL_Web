using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCLauncher.SecurityService.Entities.Interfaces
{
    internal interface IRegistrationData
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string EmailConfirmation { get; set; }
    }
}
