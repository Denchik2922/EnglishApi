using EnglishApi.Dto.DictionaryDtos;
using System.Collections.Generic;

namespace EnglishApi.Dto.UserDtos
{
    public class UserDetailsDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<DictionaryDto> EnglishDictionaries { get; set; }
    }
}
