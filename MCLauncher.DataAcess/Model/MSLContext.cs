using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MCLauncher.DataAcess.Model;

public partial class MSLContext : DbContext
{
    public MSLContext()
    {
    }

    public MSLContext(DbContextOptions<MSLContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ForgeVersion> ForgeVersions { get; set; }

    public virtual DbSet<MinecraftVersion> MinecraftVersions { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=MCLauncherDB; Trusted_Connection=True;MultipleActiveResultSets=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<ForgeVersion>(entity =>
        {
            entity.ToTable("ForgeVersions", "MSL");

            entity.Property(e => e.ForgeVersion1)
                .HasMaxLength(50)
                .HasColumnName("ForgeVersion");
            entity.Property(e => e.IdMinecraftVersion).HasColumnName("Id_MinecraftVersion");
            entity.Property(e => e.IsLatest)
                .IsRequired()
                .HasDefaultValueSql("('False')");

            entity.HasOne(d => d.IdMinecraftVersionNavigation).WithMany(p => p.ForgeVersions)
                .HasForeignKey(d => d.IdMinecraftVersion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ForgeVersions_MinecraftVersions");
        });

        modelBuilder.Entity<MinecraftVersion>(entity =>
        {
            entity.ToTable("MinecraftVersions", "MSL");

            entity.Property(e => e.IsRelease)
                .IsRequired()
                .HasDefaultValueSql("('False')");
            entity.Property(e => e.IsServerInstalled)
                .IsRequired()
                .HasDefaultValueSql("('False')");
            entity.Property(e => e.IsSnapshot)
                .IsRequired()
                .HasDefaultValueSql("('False')");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.ToTable("Servers", "MSL");

            entity.Property(e => e.AutoAgreeEula)
                .IsRequired()
                .HasDefaultValueSql("('False')")
                .HasColumnName("AutoAgreeEULA");
            entity.Property(e => e.IdForgeVersion).HasColumnName("Id_ForgeVersion");
            entity.Property(e => e.IdMinecraftVersion).HasColumnName("Id_MinecraftVersion");
            entity.Property(e => e.InsertDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsClosed)
                .IsRequired()
                .HasDefaultValueSql("('False')");
            entity.Property(e => e.IsForgeServer)
                .IsRequired()
                .HasDefaultValueSql("('False')");
            entity.Property(e => e.IsOpen)
                .IsRequired()
                .HasDefaultValueSql("('False')");
            entity.Property(e => e.IsStarting)
                .IsRequired()
                .HasDefaultValueSql("('False')");
            entity.Property(e => e.IsStopping)
                .IsRequired()
                .HasDefaultValueSql("('False')");
            entity.Property(e => e.JarfileName)
                .HasMaxLength(50)
                .HasColumnName("JARFileName");
            entity.Property(e => e.LastStartDateTime).HasColumnType("datetime");
            entity.Property(e => e.RamMaxMb).HasColumnName("RamMax(MB)");
            entity.Property(e => e.RamMinMb).HasColumnName("RamMin(MB)");
            entity.Property(e => e.ServerName).HasMaxLength(50);

            entity.HasOne(d => d.IdForgeVersionNavigation).WithMany(p => p.Servers)
                .HasForeignKey(d => d.IdForgeVersion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Servers_ForgeVersions");

            entity.HasOne(d => d.IdMinecraftVersionNavigation).WithMany(p => p.Servers)
                .HasForeignKey(d => d.IdMinecraftVersion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Servers_MiencraftVersions");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
