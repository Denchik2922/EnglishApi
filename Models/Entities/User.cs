using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public class User : IdentityUser
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public ICollection<EnglishDictionary> EnglishDictionaries { get; set; } = new List<EnglishDictionary>();
        public ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
    }
}
