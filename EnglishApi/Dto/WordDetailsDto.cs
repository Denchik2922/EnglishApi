using System.Collections.Generic;

namespace EnglishApi.Dto
{
    public class WordDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TranslatedWordDto> Translates { get; set; }
        public int EnglishDictionaryId { get; set; }
        public DictionaryDto Dictionary { get; set; }
        public string Transcription { get; set; }
        public string Picture { get; set; }
    }
}
