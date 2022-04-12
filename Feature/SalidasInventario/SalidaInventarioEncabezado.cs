namespace Productos_Backend.Feature.SalidasInventario
{
    public class SalidaInventarioEncabezado
    {
        public int salidaEncabezadoId { get; set; }
        public DateTime fecha_Salida { get; set; }
        public List<SalidaInventarioDetalle> salidaInventarioDetalles { get; set; } = new List<SalidaInventarioDetalle>();
    }
}