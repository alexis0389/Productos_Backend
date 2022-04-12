using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Productos_Backend.Infraestructure;

namespace Productos_Backend.Feature.Productos
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        private readonly ProductosDbContext _context;
        public ProductoController(ProductosDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> Get()
        {
            try
            {
                return await _context.Productos.ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> Get(int id)
        {
            try
            {
                var productoid = await _context.Productos.FindAsync(id);
                if (productoid != null)
                {
                    return productoid;
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