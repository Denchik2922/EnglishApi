using EnglishApi.Infrastructure.Validations;
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
        [EnsureMinimumElements(1, ErrorMessage = "At least a Translates is required")]
        public ICollection<TranslatedWordDto> Translates { get; set; }

        [Required]
        public int EnglishDictionaryId { get; set; }

        [Required]
        public string Transcription { get; set; }
        public string PictureUrl { get; set; }
        public string AudioUrl { get; set; }
    }
}
