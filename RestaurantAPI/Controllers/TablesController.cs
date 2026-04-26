using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly DbService _db;
        public TablesController(DbService db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tables = await _db.GetTablesAsync();
            return Ok(tables);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable()
        {
            var tables = await _db.GetTablesAsync("Available");
            return Ok(tables);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateTableStatusRequest req)
        {
            await _db.UpdateTableStatusAsync(id, req.Status);
            return Ok(new { Message = "Table status updated." });
        }
    }
}
