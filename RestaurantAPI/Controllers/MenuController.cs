using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly DbService _db;
        public MenuController(DbService db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _db.GetMenuItemsAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MenuItemRequest req)
        {
            await _db.AddMenuItemAsync(req);
            return Ok(new { Message = "Item added successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MenuItemRequest req)
        {
            await _db.UpdateMenuItemAsync(id, req);
            return Ok(new { Message = "Item updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _db.DeleteMenuItemAsync(id);
            return Ok(new { Message = "Item deleted successfully." });
        }
    }
}
