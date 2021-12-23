using System.Collections.Generic;

namespace EnglishApi.Dto
{
    public class DictionaryDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public UserDto Creator { get; set; }
        public ICollection<TagDto> Tags { get; set; } 
        public ICollection<WordDto> Words { get; set; }
        public ICollection<UserTestDto> TestUsers { get; set; }
        public bool IsPrivate { get; set; }
    }
}
