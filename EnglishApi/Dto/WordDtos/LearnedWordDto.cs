namespace EnglishApi.Dto.WordDtos
{
    public class LearnedWordDto
    {
        public int Id { get; set; }
        public bool IsLearned { get; set; }
        public string UserId { get; set; }
        public int WordId { get; set; }
    }
}
