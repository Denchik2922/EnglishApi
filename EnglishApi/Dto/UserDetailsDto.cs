using System.Collections.Generic;

namespace EnglishApi.Dto
{
    public class UserDetailsDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<DictionaryDto> EnglishDictionaries { get; set; }
        public ICollection<DictionaryTestDto> DictionarySpellingTests { get; set; }
        public ICollection<DictionaryTestDto> DictionaryMatchingTests { get; set; }
    }
}
