using Productos_Backend.Feature.Productos;

namespace Productos_Backend.Feature.SalidasInventario.DTOs
{
    public class SalidaDtos
    {
        public int loteId { get; set; }
        public int productoId { get; set; }
        public int cantidad { get; set; }
    }

}