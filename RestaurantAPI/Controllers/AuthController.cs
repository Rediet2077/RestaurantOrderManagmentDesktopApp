using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // Example DTOs (Data Transfer Objects)
        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class RegisterRequest
        {
            public string FullName { get; set; }
            public string Username { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Logic would go here to verify against the DatabaseManager
            // For now, returning a mock success response
            if (request.Email == "admin@gmail.com" && request.Password == "12345678")
            {
                return Ok(new { Token = "mock-jwt-token-admin", Role = "Admin", Message = "Login successful" });
            }

            return Unauthorized(new { Message = "Invalid email or password" });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            // Logic would go here to INSERT into the Users table
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Email and Password are required.");
            }

            // Mock success response
            return Ok(new { Message = "User registered successfully." });
        }
    }
}
