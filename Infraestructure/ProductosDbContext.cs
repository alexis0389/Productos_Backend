using Microsoft.EntityFrameworkCore;
using Productos_Backend.Feature.Productos;
using Productos_Backend.Feature.SalidasInventario;

namespace Productos_Backend.Infraestructure
{
    public class ProductosDbContext : DbContext
    {
        public ProductosDbContext(DbContextOptions<ProductosDbContext> options) : base(options)
        {
        }
        public DbSet<Producto> Productos { get; set; } = null!;
        public DbSet<ProductoLote> ProductoLotes { get; set; } = null!;
        public DbSet<SalidaInventarioEncabezado> SalidasInventarioEncabezados { get; set; } = null!;
        public DbSet<SalidaInventarioDetalle> SalidaInventarioDetalles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity => {
                entity.HasKey(p => p.ProductoId);
                entity.Property(p => p.ProductoId).HasColumnType("int");

                entity.Property(p => p.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(p => p.Total)
                    .IsRequired()
                    .HasColumnType("int")
                    .HasDefaultValue(0);
            });

            modelBuilder.Entity<ProductoLote>(entity => {
                entity.HasKey(pl => pl.LoteId);
                entity.Property(pl => pl.LoteId).HasColumnType("int");

                entity.HasOne<Producto>(p => p.Producto)
                    .WithMany(pl => pl.ProductoLote)
                    .HasForeignKey(pl => pl.ProductoId);

                entity.Property(pl => pl.fecha_cad)
                    .IsRequired()
                    .HasColumnName("Fecha_Caducidad")
                    .HasColumnType("Date");

                entity.Property(pl => pl.Cantidad)
                    .IsRequired()
                    .HasColumnType("int")
                    .HasDefaultValue(0);

                entity.Property(pl => pl.Costo)
                    .IsRequired()
                    .HasColumnType("int")
                    .HasDefaultValue(0);
            });

            modelBuilder.Entity<SalidaInventarioEncabezado>(entity => {
                entity.HasKey(s => s.salidaEncabezadoId);
                entity.Property(s => s.salidaEncabezadoId).HasColumnType("int");

                entity.Property(s => s.fecha_Salida)
                    .IsRequired()
                    .HasColumnType("Date")
                    .HasDefaultValueSql("GetDate()");
            });

            modelBuilder.Entity<SalidaInventarioDetalle>(entity => {
                entity.HasKey(sd => sd.salidaDetalleId);
                entity.Property(sd => sd.salidaDetalleId).HasColumnType("int");

                entity.HasOne<SalidaInventarioEncabezado>(s => s.SalidaInventarioEncabezado)
                    .WithMany(sd => sd.salidaInventarioDetalles)
                    .HasForeignKey(sd => sd.salidaEncabezadoId);

                entity.HasOne<Producto>(p => p.Producto)
                    .WithMany(sd => sd.Salidas)
                    .HasForeignKey(sd => sd.ProductoId);

                entity.Property(sd => sd.Cantidad)
                    .IsRequired()
                    .HasColumnType("int")
                    .HasDefaultValue(0);

                entity.Property(sd => sd.Costo)
                    .IsRequired()
                    .HasColumnType("int")
                    .HasDefaultValue(0);

                entity.Property(sd => sd.Saldo_Actual)
                    .IsRequired()
                    .HasColumnType("int")
                    .HasDefaultValue(0);
            });
        }
    }
}