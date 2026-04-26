using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DbService _db;
        public AuthController(DbService db) => _db = db;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest(new LoginResponse { Success = false, Message = "Email and password are required." });

            var user = await _db.AuthenticateAsync(request.Email, request.Password);
            if (user != null)
            {
                return Ok(new LoginResponse
                {
                    Success = true,
                    Token = "jwt-" + Guid.NewGuid().ToString("N"),
                    Role = user.Role,
                    FullName = user.FullName,
                    Email = user.Email,
                    Phone = user.Phone,
                    UserID = user.UserID,
                    CreatedAt = user.CreatedAt,
                    Message = "Login successful"
                });
            }

            return Unauthorized(new LoginResponse { Success = false, Message = "Invalid email or password." });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest(new { Success = false, Message = "Email and Password are required." });

            bool result = await _db.RegisterUserAsync(request);
            if (result)
                return Ok(new { Success = true, Message = "User registered successfully." });

            return Conflict(new { Success = false, Message = "Email already exists or conflict occurred." });
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCustomers()
        {
            int count = await _db.DeleteAllCustomersAsync();
            return Ok(new { Success = true, Message = $"Deleted {count} customers." });
        }
    }
}
