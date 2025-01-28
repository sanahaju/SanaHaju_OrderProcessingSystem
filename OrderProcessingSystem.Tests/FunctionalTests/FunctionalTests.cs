using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OrderProcessingSystem.BusinessLayer.DTO;
using OrderProcessingSystem.BusinessLayer.Interfaces;
using OrderProcessingSystem.Controllers;
using OrderProcessingSystem.Entities.Models;
using Xunit;

namespace OrderProcessingSystem.Tests.FunctionalTests
{
    public class FunctionalTests
    {
        private readonly Mock<IOrderProcessingService> _mockOrderService;
        private readonly Mock<ILogger<OrderProcessingController>> _mockLogger;
        private readonly OrderProcessingController _controller;

        public FunctionalTests()
        {
            _mockOrderService = new Mock<IOrderProcessingService>();
            _mockLogger = new Mock<ILogger<OrderProcessingController>>();
            _controller = new OrderProcessingController(_mockOrderService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllCustomers_ReturnsOkResult_WithCustomerList()
        {
            var mockCustomers = new List<Customer>
            {
                new Customer { CustomerId = 1, Name = "John Doe" },
                new Customer { CustomerId = 2, Name = "Jane Smith" }
            };
            _mockOrderService.Setup(service => service.GetAllCustomersAsync()).ReturnsAsync(mockCustomers);

            var result = await _controller.GetAllCustomers();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var customers = Assert.IsType<List<Customer>>(okResult.Value);
            Assert.Equal(2, customers.Count);
        }

        [Fact]
        public async Task GetOrderById_OrderNotFound_ReturnsNotFound()
        {
            int orderId = 123;
            _mockOrderService.Setup(service => service.GetOrderByIdAsync(orderId)).ReturnsAsync((Order)null);

            var result = await _controller.GetOrderById(orderId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Order not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task CreateOrder_ValidRequest_ReturnsCreatedAtActionResult()
        {
            var request = new CreateOrderRequestDTO
            {
                CustomerId = 1,
                ProductIds = new List<int> { 101, 102 }
            };

            var order = new Order { OrderId = 1, CustomerId = 1};
            _mockOrderService.Setup(service => service.CreateOrderAsync(request.CustomerId, request.ProductIds)).ReturnsAsync(order);

            var result = await _controller.CreateOrder(request);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetOrderById", createdAtActionResult.ActionName);
            Assert.Equal(1, createdAtActionResult.RouteValues["id"]);
        }
    }
}