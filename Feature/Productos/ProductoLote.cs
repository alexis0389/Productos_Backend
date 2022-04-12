using Productos_Backend.Feature.SalidasInventario;

namespace Productos_Backend.Feature.Productos
{
    public class ProductoLote
    {
        public int LoteId { get; set; }
        public int ProductoId { get; set; }
        public DateTime fecha_cad { get; set; }
        public int Cantidad { get; set; }
        public int Costo { get; set; }
        public virtual Producto? Producto { get; set; }
        public virtual List<SalidaInventarioDetalle> Salidas { get; set; } = new List<SalidaInventarioDetalle>();
    }
}