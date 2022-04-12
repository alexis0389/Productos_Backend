using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Productos_Backend.Feature.Productos;
using Productos_Backend.Feature.SalidasInventario.DTOs;
using Productos_Backend.Infraestructure;

namespace Productos_Backend.Feature.SalidasInventario
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalidaInventarioController : Controller
    {
        private readonly ProductosDbContext _context;
        public SalidaInventarioController(ProductosDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalidaInventarioEncabezado>>> Get()
        {
            try
            {
                return await _context.SalidasInventarioEncabezados.ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalidaInventarioEncabezado>> Get(int id)
        {
            try
            {
                var salidaid = await _context.SalidasInventarioEncabezados.FindAsync(id);
                if (salidaid != null)
                {
                    return salidaid;
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> post(List<SalidaDtos> salidaDto)
        {
            SalidaInventarioEncabezado salidaEncabezado = new SalidaInventarioEncabezado();
            salidaEncabezado.fecha_Salida = DateTime.Now.Date;
            foreach (var salida in salidaDto)
            {
                var salidaDetalle = new SalidaInventarioDetalle();
                var productMaestro = _context.Productos.AsQueryable()
                    .FirstOrDefault(p => p.ProductoId == salida.productoId);

                var product = _context.ProductoLotes.AsQueryable()
                    .FirstOrDefault(p => p.ProductoId == salida.productoId);
               
                salidaDetalle.ProductoId = salida.productoId;
                salidaDetalle.loteId = salida.loteId;
                salidaDetalle.Cantidad = salida.cantidad;
                salidaDetalle.Costo = product.Costo * salidaDetalle.Cantidad;

                var existencia = _context.ProductoLotes.AsQueryable()
                                                       .FirstOrDefault(p => p.ProductoId == salida.productoId)
                                                       .Cantidad;
                
                if (salidaDetalle.Saldo_Actual > existencia)
                {
                    return BadRequest("Error en existencia");
                }
                existencia -= salidaDetalle.Saldo_Actual;
                salidaDetalle.Saldo_Actual = existencia - salidaDetalle.Cantidad;

                salidaEncabezado.salidaInventarioDetalles.Add(salidaDetalle);

                product.Cantidad = salidaDetalle.Saldo_Actual;
                _context.ProductoLotes.Update(product);
                
                productMaestro.Total -= salidaDetalle.Cantidad;
                _context.Productos.Update(productMaestro);
            }

            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.SalidasInventarioEncabezados.Add(salidaEncabezado);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (OperationCanceledException)
            {
                await transaction.RollbackAsync();
            }
            return Ok(new {msj="Datos Creados Satisfactoriamente"});
        }
    }
}