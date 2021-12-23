﻿using System;

namespace Models
{
    public class TestResult
    {
        public int EnglishDictionaryId { get; set; }
        public EnglishDictionary EnglishDictionary { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public double Score { get; set; }
    }
}
