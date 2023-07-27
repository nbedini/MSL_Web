using System;
using System.Collections.Generic;

namespace MCLauncher.DataAcess.Model;

public partial class MinecraftVersion
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsRelease { get; set; }

    public bool? IsSnapshot { get; set; }

    public bool? IsServerInstalled { get; set; }

    public virtual ICollection<ForgeVersion> ForgeVersions { get; set; } = new List<ForgeVersion>();

    public virtual ICollection<Server> Servers { get; set; } = new List<Server>();
}
