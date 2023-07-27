using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCLauncher.SecurityService.Entities
{
    public class LoginResult
    {
        public LoginErrorStatus LoginStatus { get; set; }
        public string ErrorText { get; set; }
    }
}
