using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnglishApi.Dto
{
    public class DictionaryDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public ICollection<TagDto> Tags { get; set; }

        [Required(ErrorMessage = "User id is required")]
        public string UserId { get; set; }
        public bool IsPrivate { get; set; }
    }
}
