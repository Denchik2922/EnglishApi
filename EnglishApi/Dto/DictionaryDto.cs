namespace EnglishApi.Dto
{
    public class DictionaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public bool IsPrivate { get; set; }
    }
}
