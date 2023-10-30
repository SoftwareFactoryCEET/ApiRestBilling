using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRestBilling.Data;
using ApiRestBilling.Models;

namespace ApiRestBilling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            return await _context.Orders.Include(oi=>oi.OrderItems).ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
           // var order = await _context.Orders.Include(oi => oi.OrderItems).FindAsync(id);
            var order = await _context.Orders.Include(oi => oi.OrderItems)
                                     .FirstOrDefaultAsync(o => o.Id == id); // Asumiendo que el nombre del campo es 'Id'.

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
          if (_context.Orders == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Orders'  is null.");
          }

            //Calcular el total de la Orden de Compra
            decimal? totalOrden = 0;

            foreach (var oi in order.OrderItems)
            {
                //Buscar el producto en la base de datos por su ID
                var producto = await _context.Products.FindAsync(oi.ProductId);

                if (producto == null)
                {
                    return BadRequest($"El producto con ID {oi.ProductId} no fue encontrado");
                }

                //Asignar el precio uitario del producto al detalle.
                oi.UnitPrice = producto.UnitPrice;

                //Calcular el Subtotal de cada uno de los items de la orden
                oi.Subtotal = oi.UnitPrice * oi.Quantity;

                //Calcular el Total de la OC
                totalOrden += oi.Subtotal;
            }

            //Asignar el total calculado a la Orden de COmpra
            order.TotalAmount = Convert.ToDecimal(totalOrden);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
