using System;
using System.Collections.Generic;

namespace MCLauncher.SecurityService.DataAccess.Model;

public partial class User
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? IsLogged { get; set; }

    public DateTime? LastLoginDateTime { get; set; }

    public DateTime? LastLogoutDateTime { get; set; }

    public virtual ICollection<RolesUser> RolesUsers { get; set; } = new List<RolesUser>();
}
