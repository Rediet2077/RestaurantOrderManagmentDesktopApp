using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly DbService _db;
        public StatsController(DbService db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetStats()
        {
            try
            {
                var stats = await _db.GetDashboardStatsAsync();
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error loading stats: " + ex.Message });
            }
        }

        [HttpGet("daily-report")]
        public async Task<IActionResult> GetDailyReport()
        {
            try
            {
                var report = await _db.GetDailyReportAsync();
                return Ok(report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error loading report: " + ex.Message });
            }
        }
    }
}
