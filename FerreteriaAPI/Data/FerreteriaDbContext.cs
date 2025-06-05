using FerreteriaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FerreteriaAPI.Data
{
    public class FerreteriaDbContext : DbContext
    {
        public FerreteriaDbContext(DbContextOptions<FerreteriaDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Articulo> Articulos { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Empleado> Empleados { get; set; } = null!;
        public DbSet<FormaPago> FormasPago { get; set; } = null!;
        public DbSet<Venta> Ventas { get; set; } = null!;
        public DbSet<DetalleVenta> DetallesVenta { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar nombres de tablas explícitamente
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Articulo>().ToTable("Articulo");
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Empleado>().ToTable("Empleado");
            modelBuilder.Entity<FormaPago>().ToTable("FormaPago");
            modelBuilder.Entity<Venta>().ToTable("Venta");
            modelBuilder.Entity<DetalleVenta>().ToTable("DetalleVenta");

            // Configuración de Usuario
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.NombreUsuario)
                .IsUnique();

            // Configuración de Articulo
            modelBuilder.Entity<Articulo>()
                .HasIndex(a => a.Codigo)
                .IsUnique();

            modelBuilder.Entity<Articulo>()
                .Property(a => a.PrecioCompra)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Articulo>()
                .Property(a => a.PrecioVenta)
                .HasColumnType("decimal(10,2)");

            // Configuración de Empleado
            modelBuilder.Entity<Empleado>()
                .HasIndex(e => e.DPI)
                .IsUnique();

            modelBuilder.Entity<Empleado>()
                .Property(e => e.Salario)
                .HasColumnType("decimal(10,2)");

            // Configuración de Venta
            modelBuilder.Entity<Venta>()
                .Property(v => v.Subtotal)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Venta>()
                .Property(v => v.Impuesto)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Venta>()
                .Property(v => v.Total)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Cliente)
                .WithMany()
                .HasForeignKey(v => v.ClienteID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Empleado)
                .WithMany()
                .HasForeignKey(v => v.EmpleadoID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Venta>()
                .HasOne(v => v.FormaPago)
                .WithMany()
                .HasForeignKey(v => v.FormaPagoID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de DetalleVenta
            modelBuilder.Entity<DetalleVenta>()
                .Property(d => d.PrecioUnitario)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<DetalleVenta>()
                .Property(d => d.Subtotal)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Venta)
                .WithMany(v => v.DetallesVenta)
                .HasForeignKey(d => d.VentaID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Articulo)
                .WithMany()
                .HasForeignKey(d => d.ArticuloID)
                .OnDelete(DeleteBehavior.Restrict);

            // Datos iniciales
            modelBuilder.Entity<FormaPago>().HasData(
                new FormaPago { FormaPagoID = 1, Nombre = "Efectivo", Descripcion = "Pago en efectivo" },
                new FormaPago { FormaPagoID = 2, Nombre = "Tarjeta de Crédito", Descripcion = "Pago con tarjeta de crédito" },
                new FormaPago { FormaPagoID = 3, Nombre = "Tarjeta de Débito", Descripcion = "Pago con tarjeta de débito" },
                new FormaPago { FormaPagoID = 4, Nombre = "Transferencia Bancaria", Descripcion = "Pago mediante transferencia bancaria" }
            );

            // Usuario administrador inicial
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    UsuarioID = 1,
                    Nombre = "Admin",
                    Apellido = "Sistema",
                    NombreUsuario = "admin",
                    Contrasena = "admin123",
                    Activo = true,
                    FechaCreacion = DateTime.Now
                }
            );
        }
    }
}