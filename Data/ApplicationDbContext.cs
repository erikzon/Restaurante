using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurante.Data.Modelos;
using System.Reflection;

namespace Restaurante.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Mesa> Mesas { get; set; }
    public DbSet<Orden> Ordenes { get; set; }
    public DbSet<DetalleOrden> DetalleOrdenes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        base.OnModelCreating(modelBuilder);

        // Configurar relaciones
        modelBuilder.Entity<Producto>()
            .HasOne(p => p.Categoria)
            .WithMany(c => c.Productos)
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DetalleOrden>()
            .HasOne(d => d.Orden)
            .WithMany(o => o.DetalleOrdenes)
            .HasForeignKey(d => d.OrdenId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DetalleOrden>()
            .HasOne(d => d.Producto)
            .WithMany()
            .HasForeignKey(d => d.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Orden>()
            .HasOne(o => o.Mesa)
            .WithMany(m => m.Ordenes)
            .HasForeignKey(o => o.MesaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Datos iniciales (Seed)
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Categorías
        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { CategoriaId = 1, Nombre = "Platos Fuertes" },
            new Categoria { CategoriaId = 2, Nombre = "Bebidas" },
            new Categoria { CategoriaId = 3, Nombre = "Cerveza" },
            new Categoria { CategoriaId = 4, Nombre = "Licor" },
            new Categoria { CategoriaId = 5, Nombre = "Extras" }
        );

        // Productos
        modelBuilder.Entity<Producto>().HasData(
            new Producto { ProductoId = 1, Nombre = "Tacos (3) Pastor", Precio = 30, Codigo = "T1", CategoriaId = 1 },
            new Producto { ProductoId = 2, Nombre = "Tacos (3) Birria", Precio = 30, Codigo = "T2", CategoriaId = 1 },
            new Producto { ProductoId = 3, Nombre = "Tacos (3) Pollo", Precio = 30, Codigo = "T3", CategoriaId = 1 },
            new Producto { ProductoId = 4, Nombre = "Tacos (3) Res", Precio = 30, Codigo = "T4", CategoriaId = 1 },
            new Producto { ProductoId = 5, Nombre = "Tacos (3) Queso-Pastor", Precio = 45, Codigo = "T1Q", CategoriaId = 1 },
            new Producto { ProductoId = 6, Nombre = "Tacos (3) Queso-Birria", Precio = 45, Codigo = "T2Q", CategoriaId = 1 },
            new Producto { ProductoId = 7, Nombre = "Tacos (3) Queso-Pollo", Precio = 45, Codigo = "T3Q", CategoriaId = 1 },
            new Producto { ProductoId = 8, Nombre = "Tacos (3) Queso-Res", Precio = 45, Codigo = "T4Q", CategoriaId = 1 },

            new Producto { ProductoId = 9, Nombre = "Gringa", Precio = 35, Codigo = "G1", CategoriaId = 1 },
            new Producto { ProductoId = 10, Nombre = "Nachos de la casa", Precio = 30, Codigo = "N1", CategoriaId = 1 },
            new Producto { ProductoId = 11, Nombre = "Sopa de Birria", Precio = 30, Codigo = "N1", CategoriaId = 1 },

        );

        // Mesas
        modelBuilder.Entity<Mesa>().HasData(
            new Mesa { MesaId = 1, Numero = "1", Ocupada = false },
            new Mesa { MesaId = 2, Numero = "2", Ocupada = false },
            new Mesa { MesaId = 3, Numero = "3", Ocupada = false },
            new Mesa { MesaId = 4, Numero = "4", Ocupada = false },
            new Mesa { MesaId = 5, Numero = "5", Ocupada = false },
            new Mesa { MesaId = 6, Numero = "6", Ocupada = false }
        );
    }
}
