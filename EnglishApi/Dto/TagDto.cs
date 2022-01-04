using System.ComponentModel.DataAnnotations;

namespace EnglishApi.Dto
{
    public class TagDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
        [Required]
        [MinLength(8)]
        public string Description { get; set; }
    }
}
