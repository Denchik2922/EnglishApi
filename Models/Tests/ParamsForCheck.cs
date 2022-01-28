namespace Models.Tests
{
    public class ParamsForCheck
    {
        public TestParameters Parameters { get; set; }
        public int NextQuestion
        {
            get
            {
                if (HasNextQuestion)
                {
                    return Parameters.CurrentQuestion + 1;
                }
                else return Parameters.CurrentQuestion;
            }
        }

        public bool HasNextQuestion => Parameters.CurrentQuestion < Parameters.CountQuestion;
        public string TrueAnswer { get; set; }
        public bool IsTrueAnswer { get; set; }
    }
}
