using ApiRestBilling.Models;

namespace ApiRestBilling.Services
{
    public interface IPurchaseOrdersService
    {
        Task<decimal> CheckUnitPrice(OrderItem detalle);
        Task<decimal> CalculateSubtotalOrderItem(OrderItem item);
        decimal CalcularTotalOrderItems(List<OrderItem> item);
    }
}
