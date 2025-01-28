using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OrderProcessingSystem.BusinessLayer.DTO;
using OrderProcessingSystem.BusinessLayer.Interfaces;
using OrderProcessingSystem.Controllers;
using Xunit;

namespace OrderProcessingSystem.Tests.ValidationTests
{
    public class ValidationTests
    {
        private readonly Mock<IOrderProcessingService> _mockOrderService;
        private readonly Mock<ILogger<OrderProcessingController>> _mockLogger;
        private readonly OrderProcessingController _controller;

        public ValidationTests()
        {
            _mockOrderService = new Mock<IOrderProcessingService>();
            _mockLogger = new Mock<ILogger<OrderProcessingController>>();
            _controller = new OrderProcessingController(_mockOrderService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetCustomerById_InvalidId_ReturnsBadRequest()
        {
            int invalidCustomerId = 0;

            var result = await _controller.GetCustomerById(invalidCustomerId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid customer ID.", badRequestResult.Value);
        }

        [Fact]
        public async Task CreateOrder_InvalidInput_ReturnsBadRequest()
        {
            var request = new CreateOrderRequestDTO
            {
                CustomerId = 0,  
                ProductIds = new List<int>()  
            };

            var result = await _controller.CreateOrder(request);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid input: CustomerId and ProductIds are required.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetOrderById_InvalidId_ReturnsBadRequest()
        {
            int invalidOrderId = 0;

            var result = await _controller.GetOrderById(invalidOrderId);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid order ID.", badRequestResult.Value);
        }

        [Fact]
        public async Task CreateOrder_InvalidCustomerId_ReturnsBadRequest()
        {
            var request = new CreateOrderRequestDTO
            {
                CustomerId = -1, 
                ProductIds = new List<int> { 101, 102 }
            };

            var result = await _controller.CreateOrder(request);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid input: CustomerId and ProductIds are required.", badRequestResult.Value);
        }
    }
}