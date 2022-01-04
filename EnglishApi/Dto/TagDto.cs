using System.ComponentModel.DataAnnotations;

namespace EnglishApi.Dto
{
    public class TagDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
