namespace EnglishApi.Dto.AuthDtos
{
    public class ExternalAuthDto
    {
        public string Provider { get; set; } = "google";
        public string Token { get; set; }
    }
}
