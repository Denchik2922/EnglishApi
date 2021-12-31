using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Tests
{
    public class MatchingWordTest : IWordTest
    {
        public string WordName { get; set; }
        public string CorrectAnswer { get; set; }
        public ICollection<string> Translates { get; set; }
    }
}
