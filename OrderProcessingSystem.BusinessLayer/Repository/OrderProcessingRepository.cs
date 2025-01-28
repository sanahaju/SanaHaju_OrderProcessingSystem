using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderProcessingSystem.BusinessLayer.Interfaces;
using OrderProcessingSystem.DataLayer.Data;
using OrderProcessingSystem.Entities.Models;

namespace OrderProcessingSystem.BusinessLayer.Repository
{
    public class OrderProcessingRepository : IOrderProcessingRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<OrderProcessingRepository> _logger;

        public OrderProcessingRepository(AppDbContext appDbContext, ILogger<OrderProcessingRepository> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }
        public async Task<Order> CreateOrderAsync(int customerId, List<int> productIds)
        {
            try
            {
                _logger.LogInformation("Starting order creation for CustomerId: {CustomerId}", customerId);

                var customer = await _appDbContext.Customers
                    .Include(c => c.Orders)
                    .FirstOrDefaultAsync(c => c.CustomerId == customerId);

                if (customer == null)
                {
                    _logger.LogError("Customer not found with CustomerId: {CustomerId}", customerId);
                    throw new Exception("Customer not found");
                }

                var unfulfilledOrder = customer.Orders.FirstOrDefault(o => o.Status == OrderStatus.Pending);
                if (unfulfilledOrder != null)
                {
                    _logger.LogError("Cannot place new order. CustomerId {CustomerId} has an unfulfilled order.", customerId);
                    throw new Exception("Cannot place a new order as the customer has an unfulfilled order.");
                }

                var products = await _appDbContext.Products
                    .Where(p => productIds.Contains(p.ProductId))
                    .ToListAsync();

                if (products.Count != productIds.Count)
                {
                    _logger.LogWarning("One or more products not found for ProductIds: {ProductIds}", string.Join(",", productIds));
                    throw new Exception("One or more products were not found.");
                }

                var newOrder = new Order
                {
                    CustomerId = customerId,
                    Products = products,
                    OrderDate = DateTime.UtcNow,
                    Status = OrderStatus.Pending
                };

                _logger.LogInformation("Creating new order for CustomerId: {CustomerId} with {ProductCount} products.", customerId, products.Count);

                _appDbContext.Orders.Add(newOrder);
                await _appDbContext.SaveChangesAsync();

                _logger.LogInformation("Order created successfully with OrderId: {OrderId} for CustomerId: {CustomerId}", newOrder.OrderId, customerId);

                return newOrder;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating order for CustomerId: {CustomerId}", customerId);
                throw;
            }
        }


        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all customers.");

                var customers = await _appDbContext.Customers.Include(c => c.Orders).ToListAsync();

                if (customers == null || !customers.Any())
                {
                    _logger.LogWarning("No customers found.");
                    return new List<Customer>(); 
                }

                _logger.LogInformation("Successfully fetched {CustomerCount} customers.", customers.Count);
                return customers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all customers.");
                throw;  
            }
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            try
            {
                _logger.LogInformation("Fetching customer with CustomerId: {CustomerId}", customerId);

                var customer = await _appDbContext.Customers.Include(c => c.Orders)  .FirstOrDefaultAsync(c => c.CustomerId == customerId);

                if (customer == null)
                {
                    _logger.LogWarning("Customer not found with CustomerId: {CustomerId}", customerId);
                    throw new Exception("Customer not found.");
                }

                _logger.LogInformation("Successfully fetched customer with CustomerId: {CustomerId}", customerId);
                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching customer with CustomerId: {CustomerId}", customerId);
                throw;  
            }
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all orders.");

                var orders = await _appDbContext.Orders.Include(o => o.Customer) .Include(o => o.Products).ToListAsync();

                if (orders == null || !orders.Any())
                {
                    _logger.LogWarning("No orders found.");
                    return new List<Order>();
                }

                _logger.LogInformation("Successfully fetched {OrderCount} orders.", orders.Count);
                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all orders.");
                throw;
            }
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            try
            {
                _logger.LogInformation("Fetching order with OrderId: {OrderId}", orderId);

                var order = await _appDbContext.Orders.Include(o => o.Customer).Include(o => o.Products).FirstOrDefaultAsync(o => o.OrderId == orderId);

                if (order == null)
                {
                    _logger.LogWarning("Order not found with OrderId: {OrderId}", orderId);
                    throw new Exception("Order not found.");
                }

                _logger.LogInformation("Successfully fetched order with OrderId: {OrderId}", orderId);
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching order with OrderId: {OrderId}", orderId);
                throw; 
            }
        }
    }
}