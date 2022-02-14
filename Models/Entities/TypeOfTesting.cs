using System.Collections.Generic;

namespace Models.Entities
{
    public class TypeOfTesting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
    }
}
