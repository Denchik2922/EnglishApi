namespace Models.Entities
{
    public interface IResultTest
    {
        int EnglishDictionaryId { get; set; }
        EnglishDictionary EnglishDictionary { get; set; }
        string UserId { get; set; }
        User User { get; set; }
        double Score { get; set; }
    }
}