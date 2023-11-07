using ApiRestBilling.Data;
using ApiRestBilling.Models;

namespace ApiRestBilling.Services
{
    public class PurchaseOrdersService : IPurchaseOrdersService
    {
        private readonly ApplicationDbContext _context;
        public PurchaseOrdersService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product> GetProductById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception($"El producto con ID {productId} no fue encontrado.");
            }
            return product;
        }

        public async Task<decimal> CheckUnitPrice(OrderItem detalle)
        {
            var producto = await GetProductById(detalle.ProductId);
            detalle.UnitPrice = producto.UnitPrice;

            return (decimal)detalle.UnitPrice;
        }

        public async Task<decimal> CalculateSubtotalOrderItem(OrderItem item)
        {
            decimal unitPrice = await CheckUnitPrice(item);
            item.Subtotal = unitPrice * item.Quantity;

            return (decimal)item.Subtotal;
        }

        public decimal CalcularTotalOrderItems(List<OrderItem> items)
        {

            decimal total = 0;
            foreach (var item in items)
            {
                total += (decimal)item.Subtotal;
            }
            //decimal total = (decimal)items.Sum(item => item.Subtotal);
            return total;
        }
    }
}
