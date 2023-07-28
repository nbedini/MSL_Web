using System;
using System.Collections.Generic;
using MCLauncher.SecurityService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MCLauncher.SecurityService.DataAccess.Model;

public partial class SecurityDbContext : DbContext
{
    public SecurityDbContext()
    {
    }

    public virtual DbSet<Action> Actions { get; set; }

    public virtual DbSet<ActionsRole> ActionsRoles { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolesUser> RolesUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Statics_SecurityService.Configuration.GetConnectionString("Security_DB"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<Action>(entity =>
        {
            entity.ToTable("Actions", "Security");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<ActionsRole>(entity =>
        {
            entity.ToTable("Actions_Roles", "Security");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IdAction).HasColumnName("Id_Action");
            entity.Property(e => e.IdRole).HasColumnName("Id_Role");

            entity.HasOne(d => d.IdActionNavigation).WithMany(p => p.ActionsRoles)
                .HasForeignKey(d => d.IdAction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Actions_AR");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.ActionsRoles)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roles_AR");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Roles", "Security");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<RolesUser>(entity =>
        {
            entity.ToTable("Roles_Users", "Security");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IdRole).HasColumnName("Id_Role");
            entity.Property(e => e.IdUser).HasColumnName("Id_User");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.RolesUsers)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roles_RU");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.RolesUsers)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_RU");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users", "Security");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.IsLogged)
                .IsRequired()
                .HasDefaultValueSql("('False')");
            entity.Property(e => e.LastLoginDateTime).HasColumnType("datetime");
            entity.Property(e => e.LastLogoutDateTime).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
