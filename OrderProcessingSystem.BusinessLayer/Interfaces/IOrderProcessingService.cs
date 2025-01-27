using OrderProcessingSystem.Entities.Models;

namespace OrderProcessingSystem.BusinessLayer.Interfaces
{
    public interface IOrderProcessingService
    {
        Task<Customer> GetCustomerByIdAsync(int customerId);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Order> CreateOrderAsync(int customerId, List<int> productIds);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
    }
}
