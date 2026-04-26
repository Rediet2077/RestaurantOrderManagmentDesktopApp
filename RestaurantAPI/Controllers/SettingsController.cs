using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly DbService _db;
        public SettingsController(DbService db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var settings = await _db.GetSettingsAsync();
                return Ok(settings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Save([FromBody] SaveSettingsRequest req)
        {
            try
            {
                await _db.SaveSettingsAsync(req);
                return Ok(new { Message = "Settings saved successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}
