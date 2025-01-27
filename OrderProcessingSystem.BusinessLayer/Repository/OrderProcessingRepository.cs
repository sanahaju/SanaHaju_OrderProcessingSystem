using OrderProcessingSystem.BusinessLayer.Interfaces;
using OrderProcessingSystem.DataLayer.Data;
using OrderProcessingSystem.Entities.Models;

namespace OrderProcessingSystem.BusinessLayer.Repository
{
    public class OrderProcessingRepository : IOrderProcessingRepository
    {
        private readonly AppDbContext _appDbContext;
        public OrderProcessingRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Order> CreateOrderAsync(int customerId, List<int> productIds)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            throw new NotImplementedException();

        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            throw new NotImplementedException();

        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            throw new NotImplementedException();

        }
    }
}