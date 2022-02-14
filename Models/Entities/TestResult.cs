using Models.Entities.Interfaces;
using System;

namespace Models.Entities
{
    public class TestResult
    {
        public int Id { get; set; }
        public int EnglishDictionaryId { get; set; }
        public EnglishDictionary EnglishDictionary { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int TypeOfTestingId { get; set; }
        public TypeOfTesting Type { get; set; }
        public double Score { get; set; }
        public DateTime Date { get; set; }
    }
}
