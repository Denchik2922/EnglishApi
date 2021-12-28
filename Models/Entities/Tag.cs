using System.Collections.Generic;

namespace Models.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<EnglishDictionaryTag> EnglishDictionaries { get; set; } = new List<EnglishDictionaryTag>();  
    }
}
