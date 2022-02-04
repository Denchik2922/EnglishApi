using EnglishApi.Dto.TagDtos;
using EnglishApi.Infrastructure.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnglishApi.Dto.DictionaryDtos
{
    public class DictionaryDto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(4)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(8)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Tags is required")]
        [EnsureMinimumElements(1, ErrorMessage = "At least a Tags is required")]
        public ICollection<TagDto> Tags { get; set; }

        [Required(ErrorMessage = "User id is required")]
        public string UserId { get; set; }
        public bool IsPrivate { get; set; }
    }
}
