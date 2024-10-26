using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project9_cohort4.Server.DTOs.AuthDTOs;
using project9_cohort4.Server.Models; // Assuming you have a User model here
using project9_cohort4.Server.Services;

namespace project9_cohort4.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = _authService.Login(loginDto);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }
            var token = _authService.GenerateToken(user);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            var user = _authService.Register(registerDto);
            if (user == null)
            {
                return BadRequest("User already exists");
            }
            var token = _authService.GenerateToken(user);
            return Ok(new { token });
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetUser()
        {
            var currentUser = GetCurrentUser();
            if (currentUser == null)
            {
                return Unauthorized("User not found");
            }
            return Ok(new
            {
                Id = currentUser.UserId,
                Username = currentUser.Username,
                Email = currentUser.Email,
                Role = currentUser.IsAdmin ?? false ? "Admin" : "User"
            });
        }

        private User? GetCurrentUser()
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (userIdClaim == null)
            {
                return null;
            }
            return _authService.GetUserById(userIdClaim);
        }
        [Authorize]
        [HttpGet("get-token")]
        public IActionResult GetToken()
        {
            // Get the token from the Authorization header
            var token = HttpContext.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                // Remove "Bearer " from the token string
                token = token.Substring("Bearer ".Length);
            }
            else
            {
                return Unauthorized("Token is missing or invalid");
            }

            return Ok(new { Token = token });
        }
    }
}

