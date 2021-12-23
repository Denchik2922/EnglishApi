namespace Models
{
    public class EnglishDictionaryTag
    {
        public int EnglishDictionaryId { get; set; }
        public EnglishDictionary EnglishDictionary { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
