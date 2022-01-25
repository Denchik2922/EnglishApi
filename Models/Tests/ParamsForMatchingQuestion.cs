using System.Collections.Generic;

namespace Models.Tests
{
    public class ParamsForMatchingQuestion
    {
        public TestParameters Parameters { get; set; }
        public ICollection<string> Translates { get; set; }
        public string WordName { get; set; }
    }
}
