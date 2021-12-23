﻿using System.Collections.Generic;

namespace EnglishApi.Dto
{
    public class TagDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<DictionaryDto> EnglishDictionaries { get; set; }
    }
}
