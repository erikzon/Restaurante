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
            new Categoria { CategoriaId = 1, Nombre = "Entradas" },
            new Categoria { CategoriaId = 2, Nombre = "Platos Fuertes" },
            new Categoria { CategoriaId = 3, Nombre = "Bebidas" },
            new Categoria { CategoriaId = 4, Nombre = "Postres" },
            new Categoria { CategoriaId = 5, Nombre = "Combos" }
        );

        // Productos
        modelBuilder.Entity<Producto>().HasData(
            new Producto { ProductoId = 1, Nombre = "Hamburguesa", Precio = 120.00m, Codigo = "P1", CategoriaId = 2 },
            new Producto { ProductoId = 2, Nombre = "Pizza", Precio = 150.00m, Codigo = "P2", CategoriaId = 2 },
            new Producto { ProductoId = 3, Nombre = "Ensalada", Precio = 80.00m, Codigo = "P3", CategoriaId = 1 },
            new Producto { ProductoId = 4, Nombre = "Pasta", Precio = 110.00m, Codigo = "P4", CategoriaId = 2 },
            new Producto { ProductoId = 5, Nombre = "Tacos", Precio = 95.00m, Codigo = "P5", CategoriaId = 2 },
            new Producto { ProductoId = 6, Nombre = "Papas Fritas", Precio = 90.00m, Codigo = "P6", CategoriaId = 1 },
            new Producto { ProductoId = 7, Nombre = "Refresco", Precio = 35.00m, Codigo = "B1", CategoriaId = 3 },
            new Producto { ProductoId = 8, Nombre = "Café", Precio = 25.00m, Codigo = "B2", CategoriaId = 3 },
            new Producto { ProductoId = 9, Nombre = "Agua", Precio = 20.00m, Codigo = "B3", CategoriaId = 3 },
            new Producto { ProductoId = 10, Nombre = "Pastel", Precio = 45.00m, Codigo = "D1", CategoriaId = 4 },
            new Producto { ProductoId = 11, Nombre = "Helado", Precio = 40.00m, Codigo = "D2", CategoriaId = 4 },
            new Producto { ProductoId = 12, Nombre = "Flan", Precio = 35.00m, Codigo = "D3", CategoriaId = 4 }
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
