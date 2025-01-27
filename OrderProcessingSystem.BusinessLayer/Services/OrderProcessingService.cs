using Castle.Core.Resource;
using OrderProcessingSystem.BusinessLayer.Interfaces;
using OrderProcessingSystem.DataLayer.Data;
using OrderProcessingSystem.Entities.Models;

namespace OrderProcessingSystem.BusinessLayer.Services
{
    public class OrderProcessingService : IOrderProcessingService
    {
        private readonly IOrderProcessingRepository _orderProcessingRepository;

        public OrderProcessingService(IOrderProcessingRepository orderProcessingRepository)
        {
            _orderProcessingRepository = orderProcessingRepository;
        }

        public async Task<Order> CreateOrderAsync(int customerId, List<int> productIds)
        {
            return await _orderProcessingRepository.CreateOrderAsync(customerId, productIds);   
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _orderProcessingRepository.GetAllCustomersAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderProcessingRepository.GetAllOrdersAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _orderProcessingRepository.GetCustomerByIdAsync(customerId);
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderProcessingRepository.GetOrderByIdAsync(orderId);
        }
    }
}
