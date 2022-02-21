using EnglishApi.Dto.ExamplesWordDtos;
using EnglishApi.Dto.TranslatesDtos;
using EnglishApi.Infrastructure.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnglishApi.Dto.WordDtos
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
        [EnsureMinimumElements(1, ErrorMessage = "At least a ExampleWords is required")]
        public ICollection<ExampleWordDto> WordExamples { get; set; }

        [Required]
        public int EnglishDictionaryId { get; set; }

        [Required]
        public string Transcription { get; set; }
        public string PictureUrl { get; set; }
        public string AudioUrl { get; set; }
    }
}
