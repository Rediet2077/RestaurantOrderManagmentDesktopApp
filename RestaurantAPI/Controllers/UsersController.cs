using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DbService _db;
        public UsersController(DbService db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var staff = await _db.GetStaffAsync();
            return Ok(staff);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddStaffRequest req)
        {
            try
            {
                await _db.AddStaffAsync(req);
                return Ok(new { Message = "Staff added successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error adding staff: " + ex.Message });
            }
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> Delete(string username)
        {
            if (username.Equals("admin", StringComparison.OrdinalIgnoreCase))
                return BadRequest(new { Message = "Cannot delete admin." });

            await _db.DeleteStaffAsync(username);
            return Ok(new { Message = "Staff removed." });
        }
    }
}
