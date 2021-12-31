using System.Collections.Generic;

namespace Models.Tests
{
    public interface IWordTest
    {
        string WordName { get; set; }
        public string CorrectAnswer { get; set; }
    }
}