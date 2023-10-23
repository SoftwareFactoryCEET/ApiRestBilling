using ApiRestBilling.Data;
using ApiRestBilling.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestBilling.Controllers
{
    [Route("api/[controller]")] // Framework de ruteo. Ámbito Global
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SuppliersController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: api/<SuppliersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> Get()
        {
            if (_context.Suppliers == null)
            {
                return NotFound();
            }
            return await _context.Suppliers.ToListAsync();
        }
        

        // GET api/<SuppliersController>/5
        [HttpGet("{id}")]   
        public async Task<ActionResult<Supplier>> Get(int id)
        {
            if (_context.Suppliers == null)
            {
                return NotFound();
            }
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier is null)
            {
                return NotFound();
            }

            return supplier;
        }

        // POST api/<SuppliersController>
        [HttpPost]
        public async Task<ActionResult<Supplier>> Post([FromBody] Supplier supplier)
        {
            if (_context.Suppliers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Suppliers'  is null.");
            }
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return  CreatedAtAction("Get", new {id = supplier.Id}, supplier);
        }

        // PUT api/<SuppliersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SuppliersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
