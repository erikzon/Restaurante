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
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DetalleOrden>()
            .HasOne(d => d.Orden)
            .WithMany(o => o.DetalleOrdenes)
            .HasForeignKey(d => d.OrdenId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DetalleOrden>()
            .HasOne(d => d.Producto)
            .WithMany()
            .HasForeignKey(d => d.ProductoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Orden>()
            .HasOne(o => o.Mesa)
            .WithMany(m => m.Ordenes)
            .HasForeignKey(o => o.MesaId)
            .OnDelete(DeleteBehavior.Cascade);

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
            new Producto { ProductoId = 11, Nombre = "Sopa de Birria", Precio = 25, Codigo = "S1", CategoriaId = 1 },
            new Producto { ProductoId = 12, Nombre = "Cevinachos", Precio = 35, Codigo = "N2", CategoriaId = 1 },
            new Producto { ProductoId = 13, Nombre = "Ceviche Tradicional", Precio = 70, Codigo = "C1", CategoriaId = 1 },
            new Producto { ProductoId = 14, Nombre = "Ceviche Peruano", Precio = 70, Codigo = "C2", CategoriaId = 1 },
            new Producto { ProductoId = 15, Nombre = "Ceviche Aguachile", Precio = 70, Codigo = "C3", CategoriaId = 1 },
            new Producto { ProductoId = 16, Nombre = "Camarones al Ajillo", Precio = 70, Codigo = "Ca1", CategoriaId = 1 },

            new Producto { ProductoId = 17, Nombre = "Jamaica", Precio = 15, Codigo = "b1", CategoriaId = 2 },
            new Producto { ProductoId = 18, Nombre = "Crema", Precio = 15, Codigo = "b2", CategoriaId = 2 },
            new Producto { ProductoId = 19, Nombre = "Horchata", Precio = 15, Codigo = "b3", CategoriaId = 2 },
            new Producto { ProductoId = 20, Nombre = "Tamarindo", Precio = 15, Codigo = "b4", CategoriaId = 2 },
            new Producto { ProductoId = 21, Nombre = "Limonada", Precio = 15, Codigo = "b5", CategoriaId = 2 },
            new Producto { ProductoId = 22, Nombre = "Sodas", Precio = 10, Codigo = "b6", CategoriaId = 2 },
            new Producto { ProductoId = 23, Nombre = "Mineral preparada", Precio = 20, Codigo = "b7", CategoriaId = 2 },
            new Producto { ProductoId = 24, Nombre = "Mineral con jugo", Precio = 25, Codigo = "b8", CategoriaId = 2 },
            new Producto { ProductoId = 25, Nombre = "Agua Pura", Precio = 5, Codigo = "b9", CategoriaId = 2 },
            new Producto { ProductoId = 26, Nombre = "Cafe", Precio = 10, Codigo = "b10", CategoriaId = 2 },

            new Producto { ProductoId = 27, Nombre = "Gallo", Precio = 15, Codigo = "cer-1", CategoriaId = 3 },
            new Producto { ProductoId = 28, Nombre = "Corona", Precio = 20, Codigo = "cer-2", CategoriaId = 3 },
            new Producto { ProductoId = 29, Nombre = "Cubetazo Gallo", Precio = 75, Codigo = "cer-3", CategoriaId = 3 },
            new Producto { ProductoId = 30, Nombre = "Cubetazo Corona", Precio = 100, Codigo = "cer-4", CategoriaId = 3 },
            new Producto { ProductoId = 31, Nombre = "Michelada", Precio = 35, Codigo = "cer-5", CategoriaId = 3 },
            new Producto { ProductoId = 32, Nombre = "Picona Gallo", Precio = 18, Codigo = "cer-6", CategoriaId = 3 },
            new Producto { ProductoId = 33, Nombre = "Picona Tecate", Precio = 15, Codigo = "cer-7", CategoriaId = 3 },

            new Producto { ProductoId = 34, Nombre = "Suerito", Precio = 25, Codigo = "lic-1", CategoriaId = 4 },
            new Producto { ProductoId = 35, Nombre = "Botella Venado Light", Precio = 170, Codigo = "lic-2", CategoriaId = 4 },
            new Producto { ProductoId = 36, Nombre = "1/2 Venado Light o XL", Precio = 85, Codigo = "lic-3", CategoriaId = 4 },
            new Producto { ProductoId = 37, Nombre = "Quetzalteca preparada", Precio = 25, Codigo = "lic-4", CategoriaId = 4 },
            new Producto { ProductoId = 38, Nombre = "Botella old parr", Precio = 525, Codigo = "lic-5", CategoriaId = 4 },
            new Producto { ProductoId = 39, Nombre = "Botella Buchanan's", Precio = 550, Codigo = "lic-6", CategoriaId = 4 },
            new Producto { ProductoId = 40, Nombre = "Red Label", Precio = 250, Codigo = "lic-7", CategoriaId = 4 },
            new Producto { ProductoId = 41, Nombre = "Tequila Jose Cuervo", Precio = 250, Codigo = "lic-8", CategoriaId = 4 },
            new Producto { ProductoId = 42, Nombre = "Tequila Don Julio 70", Precio = 750, Codigo = "lic-9", CategoriaId = 4 },
            new Producto { ProductoId = 43, Nombre = "Botella Jagermeister", Precio = 500, Codigo = "lic-10", CategoriaId = 4 },

            new Producto { ProductoId = 44, Nombre = "Helado", Precio = 25, Codigo = "ex-1", CategoriaId = 5 }
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
