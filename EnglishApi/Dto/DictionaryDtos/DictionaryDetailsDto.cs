using EnglishApi.Dto.TagDtos;
using EnglishApi.Dto.UserDtos;
using EnglishApi.Dto.WordDtos;
using System.Collections.Generic;

namespace EnglishApi.Dto.DictionaryDtos
{
    public class DictionaryDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public ICollection<TagDto> Tags { get; set; }
        public ICollection<WordDto> Words { get; set; }
        public ICollection<UserTestDto> SpellingTestUsers { get; set; }
        public ICollection<UserTestDto> MatchingTestUsers { get; set; }
        public bool IsPrivate { get; set; }
    }
}
