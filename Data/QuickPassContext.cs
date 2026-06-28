using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using Quickpass.Api.Models;

namespace Quickpass.Api.Data;

public class QuickPassContext : DbContext
{
    public QuickPassContext( DbContextOptions<QuickPassContext> options ) : base(options)
    {
        
    }

    public DbSet<Usuario> Usuarios  => Set<Usuario>();

    public DbSet<Rol> Roles => Set<Rol>();

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

       modelBuilder.Entity<Usuario>().HasOne(u => u.Rol).WithMany(r =>  r.Usuarios).HasForeignKey(u => u.RolId);
       
       modelBuilder.Entity<Usuario>().Property(u => u.Password).HasColumnName("Password");
    }
}