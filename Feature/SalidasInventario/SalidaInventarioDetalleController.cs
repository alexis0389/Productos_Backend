using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Productos_Backend.Infraestructure;

namespace Productos_Backend.Feature.SalidasInventario
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalidaInventarioDetalleController : Controller
    {
        private readonly ProductosDbContext _context;
        public SalidaInventarioDetalleController(ProductosDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalidaInventarioDetalle>>> Get()
        {
            try
            {
                return await _context.SalidaInventarioDetalles.ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalidaInventarioDetalle>> Get(int id)
        {
            try
            {
                var salidaid = await _context.SalidaInventarioDetalles.FindAsync(id);
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
    }
}