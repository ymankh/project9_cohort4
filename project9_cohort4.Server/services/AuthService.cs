using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using project9_cohort4.Server.DTOs.AuthDTOs;
using project9_cohort4.Server.Models;

namespace project9_cohort4.Server.Services
{
    public class AuthService
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(MyDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public User? Login(LoginDto loginDto)
        {
            var hashedPassword = PasswordHasher.HashPassword(loginDto.Password);
            var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email && u.PasswordHash == hashedPassword);
            return user;
        }

        public User? Register(RegisterDto registerDto)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == registerDto.Email);
            if (existingUser != null)
            {
                return null;
            }

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = PasswordHasher.HashPassword(registerDto.Password),
                FullName = registerDto.FullName,
                IsAdmin = false
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.IsAdmin?? false ? "Admin" : "User"),
                new Claim("userId", user.UserId.ToString()) // Custom claim for user ID
            };

            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var secretKey = _config["Jwt:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User? GetUserById(string userId)
        {
            return _context.Users.Find(Convert.ToInt32(userId));
        }
    }
}
