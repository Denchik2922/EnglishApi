using Models.Entities.Interfaces;
using System.Collections.Generic;

namespace Models.Entities
{
    public class Word : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<LearnedWord> LearnedWords { get; set; } = new List<LearnedWord>();
        public ICollection<TranslatedWord> Translates { get; set; } = new List<TranslatedWord>();
        public ICollection<WordExample> WordExamples { get; set; } = new List<WordExample>();
        public int EnglishDictionaryId { get; set; }
        public EnglishDictionary Dictionary { get; set; }
        public string Transcription { get; set; }
        public string PictureUrl { get; set; }
        public string AudioUrl { get; set; }
    }
}
