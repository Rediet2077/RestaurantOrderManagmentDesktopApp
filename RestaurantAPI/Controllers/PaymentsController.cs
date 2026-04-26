using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly DbService _db;
        public PaymentsController(DbService db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> Process([FromBody] ProcessPaymentRequest req)
        {
            try
            {
                bool success = await _db.ProcessPaymentAsync(req);
                if (success)
                    return Ok(new { Message = "Payment processed successfully." });
                else
                    return BadRequest(new { Message = "Payment failed. Please verify order status." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Payment failed: " + ex.Message });
            }
        }

        [HttpPost("receipt")]
        public async Task<IActionResult> SubmitReceipt([FromBody] ReceiptRequest req)
        {
            try
            {
                await _db.AddReceiptAsync(req);
                return Ok(new { Message = "Receipt saved and sent to admin." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to save receipt: " + ex.Message });
            }
        }

        [HttpGet("receipts")]
        public async Task<IActionResult> GetReceipts()
        {
            try
            {
                var receipts = await _db.GetReceiptsAsync();
                return Ok(receipts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to fetch receipts: " + ex.Message });
            }
        }
    }
}
