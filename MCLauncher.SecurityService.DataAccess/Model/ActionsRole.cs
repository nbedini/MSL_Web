using System;
using System.Collections.Generic;

namespace MCLauncher.SecurityService.DataAccess.Model;

public partial class ActionsRole
{
    public long Id { get; set; }

    public long IdRole { get; set; }

    public long IdAction { get; set; }

    public virtual Action IdActionNavigation { get; set; } = null!;

    public virtual Role IdRoleNavigation { get; set; } = null!;
}
