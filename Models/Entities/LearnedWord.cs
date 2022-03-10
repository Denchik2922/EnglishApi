namespace Models.Entities
{
    public class LearnedWord
    {
        public int Id { get; set; }
        public int CountTrueAnswers { get; set; }
        public bool IsLearned { get; set; }
        public int WordId { get; set; }
        public Word Word { get; set; }
        public string UserId { get; set; }
    }
}
