using Productos_Backend.Feature.SalidasInventario;

namespace Productos_Backend.Feature.Productos
{
    public class Producto
    {
        public int ProductoId { get; set; }
        public string? Nombre { get; set; }
        public int Total { get; set; }
        public virtual List<ProductoLote> ProductoLote { get; set; } = new List<ProductoLote>();
        public virtual List<SalidaInventarioDetalle> Salidas { get; set; } = new List<SalidaInventarioDetalle>();
    }
}