using System.Collections.Generic;

namespace EnglishApi.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<DictionaryDto> EnglishDictionaries { get; set; }
        public ICollection<DictionaryTestDto> DictionaryTests { get; set; }

    }
}
