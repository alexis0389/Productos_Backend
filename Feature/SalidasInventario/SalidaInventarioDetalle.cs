using Productos_Backend.Feature.Productos;

namespace Productos_Backend.Feature.SalidasInventario
{
    public class SalidaInventarioDetalle
    {
        public int salidaDetalleId { get; set; }
        public int salidaEncabezadoId { get; set; }
        public int loteId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public int Costo { get; set; }
        public int Saldo_Actual { get; set; }
        public ProductoLote? ProductoLotes { get; set; }
        public SalidaInventarioEncabezado? SalidaInventarioEncabezado { get; set; }
        public Producto? Producto { get; set; }
    }
}