using Castle.Core.Resource;
using Microsoft.Extensions.Logging;
using OrderProcessingSystem.BusinessLayer.Interfaces;
using OrderProcessingSystem.DataLayer.Data;
using OrderProcessingSystem.Entities.Models;

namespace OrderProcessingSystem.BusinessLayer.Services
{
    public class OrderProcessingService : IOrderProcessingService
    {
        private readonly IOrderProcessingRepository _orderProcessingRepository;
        private readonly ILogger<OrderProcessingService> _logger;

        public OrderProcessingService(IOrderProcessingRepository orderProcessingRepository, ILogger<OrderProcessingService> logger)
        {
            _orderProcessingRepository = orderProcessingRepository;
            _logger = logger;
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
