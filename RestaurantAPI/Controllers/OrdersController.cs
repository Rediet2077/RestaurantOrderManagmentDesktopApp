using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DbService _db;
        public OrdersController(DbService db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest req)
        {
            try
            {
                int orderId = await _db.CreateOrderAsync(req);
                return Ok(new { OrderID = orderId, Message = "Order placed successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Order failed: " + ex.Message });
            }
        }

        [HttpGet("pending")]
        public async Task<IActionResult> GetPending()
        {
            var orders = await _db.GetPendingOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("customer/{userId}")]
        public async Task<IActionResult> GetCustomerOrders(int userId)
        {
            var orders = await _db.GetCustomerOrdersAsync(userId);
            return Ok(orders);
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var details = await _db.GetOrderDetailsAsync(id);
            return Ok(details);
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _db.GetCustomersAsync();
            return Ok(customers);
        }
    }
}
