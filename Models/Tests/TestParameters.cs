using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Tests
{
    public class TestParameters
    {
        public double Score { get; set; }
        public int CountQuestion { get; set; } 
        public int DictionaryId { get; set; }
        public int CurrentQuestion { get; set; }
        public int CountWord { get; set; }
        public int NextQuestion
        {
            get {
                if (HasNextQuestion)
                {
                    return CurrentQuestion + 1;
                }
                else return CurrentQuestion;
            }
        }
        public bool HasNextQuestion => CurrentQuestion < CountQuestion;
    }
}
