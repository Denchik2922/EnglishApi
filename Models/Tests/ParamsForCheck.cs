using System;
using System.Collections.Generic;
using System.Linq;
namespace Models.Tests
{
    public class ParamsForCheck : TestParameters
    {
        public int NextQuestion
        {
            get
            {
                if (HasNextQuestion)
                {
                    return CurrentQuestion + 1;
                }
                else return CurrentQuestion;
            }
        }

        public bool HasNextQuestion => CurrentQuestion < CountQuestion;
        public string TrueAnswer { get; set; }
        public bool IsTrueAnswer { get; set; }
    }
}
