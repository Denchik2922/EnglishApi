using System.Collections.Generic;

namespace Models.Tests
{
    public class MultipleMatchingQuestion
    {
        public TestParameters Parameters { get; set; }
        public ICollection<string> Translates { get; set; }
        public ICollection<string> WordNames { get; set; }
    }
}
