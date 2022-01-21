using System.ComponentModel.DataAnnotations;

namespace EnglishApi.Dto
{
    public class TagDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
