using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Productos_Backend.Infraestructure;

namespace Productos_Backend.Feature.Productos
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoLoteController : Controller
    {
        private readonly ProductosDbContext _context;
        public ProductoLoteController(ProductosDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoLote>>> Get()
        {
            try
            {
                return await _context.ProductoLotes.ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{productoId}")]
        public async Task<ActionResult<List<ProductoLote>>> Productos(int productoId)
        {
            try
            {
                var productos = await _context.ProductoLotes.Where(p => p.ProductoId == productoId).ToListAsync();
                if (productos != null)
                {
                    return productos;
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