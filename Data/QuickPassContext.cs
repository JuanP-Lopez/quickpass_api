using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using Quickpass.Api.Models;

namespace Quickpass.Api.Data;

public class QuickPassContext : DbContext
{
    public QuickPassContext(DbContextOptions<QuickPassContext> options) : base(options)
    {

    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();

    public DbSet<Rol> Roles => Set<Rol>();

    public DbSet<Evento> Eventos => Set<Evento>();

    public DbSet<Slot> Slots => Set<Slot>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>().ToTable("usuarios");

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.ToTable("roles");
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Id).HasColumnName("Id");
            entity.Property(r => r.RolNombre).HasColumnName("Rol");
        });

        modelBuilder.Entity<Evento>().ToTable("eventos");

        modelBuilder.Entity<Slot>().ToTable("slots");

        modelBuilder.Entity<Usuario>().HasOne(u => u.Rol).WithMany(r => r.Usuarios).HasForeignKey(u => u.RolId);

        modelBuilder.Entity<Usuario>().Property(u => u.Password).HasColumnName("Password");

        modelBuilder.Entity<Evento>().HasOne<Usuario>().WithMany().HasForeignKey(e => e.Id_Administrador);

        modelBuilder.Entity<Slot>().HasOne(s => s.Evento).WithMany(e => e.Slots).HasForeignKey(s => s.id_evento);
        
        modelBuilder.Entity<Slot>().HasOne(s => s.usuario).WithMany().HasForeignKey(s => s.Id_Usuario).OnDelete(DeleteBehavior.SetNull);
    }
}