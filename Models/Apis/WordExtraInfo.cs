using System.Collections.Generic;

namespace Models.Apis
{
    public class WordExtraInfo
    {
        public ICollection<string> WordExamples { get; set; }
        public WordPhonetic WordPhonetic { get; set; }
    }
}
