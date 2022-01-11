﻿using System.ComponentModel.DataAnnotations;

namespace EnglishApi.Dto
{
    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
