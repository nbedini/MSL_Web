using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCLauncher.SecurityService.Entities
{
    public enum LoginErrorStatus: int
    {
        Success = 0,
        UserNotFound = 1,
        UserAlreadyLogged = 2
    }

    public enum LogoutErrorStatus : int
    {
        Success = 0,
        UserNotFound = 1,
        UserNotLogged = 2
    }
}
