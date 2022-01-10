using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class ResultForMatchingTest
    {
        public int EnglishDictionaryId { get; set; }
        public EnglishDictionary EnglishDictionary { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public double Score { get; set; }
    }
}
