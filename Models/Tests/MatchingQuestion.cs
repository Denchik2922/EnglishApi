using System.Collections.Generic;

namespace Models.Tests
{
    public class MatchingQuestion
    {
        public TestParameters Parameters { get; set; }
        public ICollection<string> Translates { get; set; }
        public string WordName { get; set; }
    }
}
