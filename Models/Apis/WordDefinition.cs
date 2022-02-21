using Newtonsoft.Json;

namespace Models.Apis
{
    public class WordDefinition
    {
        [JsonProperty("definition")]
        public string Definition { get; set; }

        [JsonProperty("example")]
        public string Example { get; set; }
    }
}
