using APILibreriaF.Data;
using APILibreriaF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APILibreriaF.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ApplicationDBContext _db;

        public ProductoController(ApplicationDBContext db)
        {
            _db = db;
        }

        // GET: api/<ProductoController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Producto> products = await _db.producto.ToListAsync();
            return Ok(products);
        }

        // GET api/<ProductoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Producto producto = await _db.producto.FirstOrDefaultAsync(x => x.IdProducto == id);
            if (producto == null)
            {
                return BadRequest();
            }

            return Ok(producto);

        }

        // POST api/<ProductoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Producto producto)
        {
            Producto producto2 = await _db.producto.FirstOrDefaultAsync(x => x.IdProducto == producto.IdProducto);
            if (producto == null || producto2 != null)
            {
                return BadRequest("Ya existe :( ");
            }
            else
            {
                await _db.producto.AddAsync(producto);
                await _db.SaveChangesAsync();
                return Ok(producto);
            }

        }

        // PUT api/<ProductoController>/5
        [HttpPut("{IdProducto}")]
        public async Task<IActionResult> Put(int IdProducto, [FromBody] Producto producto)
        {
            Producto producto2 = await _db.producto.FirstOrDefaultAsync(x => x.IdProducto == IdProducto);
            if (producto == null && producto2 != null)
            {
                return NotFound();
            }
            else
            {

                producto2.Cantidad = producto.Cantidad != null ? producto.Cantidad : producto2.Cantidad;
                producto2.Descripcion = producto.Descripcion != null ? producto.Descripcion : producto2.Descripcion;
                producto2.Nombre = producto.Nombre != null ? producto.Nombre : producto2.Nombre;
                _db.producto.Update(producto2);
                await _db.SaveChangesAsync();
                return Ok();
            }

        }

        // DELETE api/<ProductoController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Producto producto = await _db.producto.FirstOrDefaultAsync(x => x.IdProducto == id);
            if (producto == null)
            {
                return BadRequest();
            }
            else
            {
                _db.producto.Remove(producto);
                await _db.SaveChangesAsync();
                return NoContent();
            }

        }
    }
}
