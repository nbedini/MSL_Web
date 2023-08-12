using System;
using System.Collections.Generic;

namespace MCLauncher.WebUI.Entities;

public partial class ForgeVersion
{
    public int Id { get; set; }

    public int IdMinecraftVersion { get; set; }

    public string ForgeVersion1 { get; set; } = null!;

    public bool? IsLatest { get; set; }

    public virtual MinecraftVersion IdMinecraftVersionNavigation { get; set; } = null!;

    public virtual ICollection<Server> Servers { get; set; } = new List<Server>();
}
