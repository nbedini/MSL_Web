using System;
using System.Collections.Generic;

namespace MCLauncher.DataAcess.Model;

public partial class Server
{
    public int Id { get; set; }

    public DateTime InsertDateTime { get; set; }

    public string ServerName { get; set; } = null!;

    public int IdMinecraftVersion { get; set; }

    public bool? IsOpen { get; set; }

    public bool? IsStarting { get; set; }

    public bool? IsClosed { get; set; }

    public bool? IsForgeServer { get; set; }

    public int IdForgeVersion { get; set; }

    public string InstallDirectory { get; set; } = null!;

    public string JarfileName { get; set; } = null!;

    public bool? AutoAgreeEula { get; set; }

    public int RamMinMb { get; set; }

    public int RamMaxMb { get; set; }

    public DateTime? LastStartDateTime { get; set; }

    public short NumPlayerOnline { get; set; }

    public virtual ForgeVersion IdForgeVersionNavigation { get; set; } = null!;

    public virtual MinecraftVersion IdMinecraftVersionNavigation { get; set; } = null!;
}
