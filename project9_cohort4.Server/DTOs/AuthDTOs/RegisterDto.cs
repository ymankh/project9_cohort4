using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project9_cohort4.Server.DTOs.AuthDTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        [MinLength(4)]
        public string Password { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string? FullName { get; set; }

    }
}