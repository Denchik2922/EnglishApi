using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnglishApi.Dto
{
    public class WordDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ICollection<TranslatedWordDto> Translates { get; set; }

        [Required]
        public int EnglishDictionaryId { get; set; }
        public string Transcription { get; set; }
        public string Picture { get; set; }
        public string Audio { get; set; }
    }
}
