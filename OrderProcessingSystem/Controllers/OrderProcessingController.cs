using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderProcessingSystem.BusinessLayer.DTO;
using OrderProcessingSystem.BusinessLayer.Interfaces;

namespace OrderProcessingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProcessingController : ControllerBase
    {
        private readonly IOrderProcessingService _orderService;
        private readonly ILogger<OrderProcessingController> _logger;

        public OrderProcessingController(IOrderProcessingService orderService, ILogger<OrderProcessingController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _orderService.GetAllCustomersAsync();
                if (customers == null || !customers.Any())
                {
                    return NotFound("No customers found.");
                }
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all customers.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("customers/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid customer ID.");
                }

                var customer = await _orderService.GetCustomerByIdAsync(id);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching customer with ID: {CustomerId}", id);
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("orders")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDTO request)
        {
            try
            {
                if (request.CustomerId <= 0 || request.ProductIds == null || !request.ProductIds.Any())
                {
                    return BadRequest("Invalid input: CustomerId and ProductIds are required.");
                }

                var order = await _orderService.CreateOrderAsync(request.CustomerId, request.ProductIds);

                return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating order for CustomerId: {CustomerId}", request.CustomerId);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("orders/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid order ID.");
                }

                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    return NotFound("Order not found.");
                }

                return Ok(new
                {
                    Order = order,
                    TotalPrice = order.TotalPrice
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching order with OrderId: {OrderId}", id);
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
