using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Models.Entities
{
    public class User : IdentityUser
    {
        public ICollection<EnglishDictionary> EnglishDictionaries { get; set; } = new List<EnglishDictionary>();
        public ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
    }
}
