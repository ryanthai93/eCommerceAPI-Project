﻿using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebAPI.Models
{
    public class RegisterDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public string RegistrationCode { get; set; }
    }
}
