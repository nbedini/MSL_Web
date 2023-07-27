using System;
using System.Collections.Generic;

namespace MCLauncher.SecurityService.DataAccess.Model;

public partial class Action
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ActionsRole> ActionsRoles { get; set; } = new List<ActionsRole>();
}
