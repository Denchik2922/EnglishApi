using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public class EnglishDictionary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; } 
        public User Creator { get; set; }
        public ICollection<EnglishDictionaryTag> Tags { get; set; } = new List<EnglishDictionaryTag>();
        public ICollection<Word> Words { get; set; } = new List<Word>();
        public ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
        public bool IsPrivate { get; set; }
    }
}
