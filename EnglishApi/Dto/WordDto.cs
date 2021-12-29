using System.Collections.Generic;

namespace EnglishApi.Dto
{
    public class WordDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TranslatedWordDto> Translates { get; set; }
        public int EnglishDictionaryId { get; set; }
        public string Transcription { get; set; }
        public string Picture { get; set; }

        public string Audio { get; set; }
    }
}
