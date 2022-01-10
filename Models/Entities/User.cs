using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Models.Entities
{
    public class User : IdentityUser
    {
        public ICollection<EnglishDictionary> EnglishDictionaries { get; set; } = new List<EnglishDictionary>();
        public ICollection<ResultForSpellingTest> SpellingTestResults { get; set; } = new List<ResultForSpellingTest>();
        public ICollection<ResultForMatchingTest> MatchingTestResults { get; set; } = new List<ResultForMatchingTest>();
    }
}
