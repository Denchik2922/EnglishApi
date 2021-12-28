using System.Collections.Generic;

namespace Models.Entities
{
    public class Word
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TranslatedWord> Translates { get; set; } = new List<TranslatedWord>();
        public int EnglishDictionaryId { get; set; }
        public EnglishDictionary Dictionary { get; set; }
        public string Transcription { get; set; }
        public string PictureUrl { get; set; }
        public string AudioUrl { get; set; }
    }
}
