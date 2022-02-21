using Newtonsoft.Json;
using System.Collections.Generic;

namespace Models.Apis
{
    public class WordMeaning
    {
        [JsonProperty("partOfSpeech")]
        public string PartOfSpeech { get; set; }

        [JsonProperty("definitions")]
        public ICollection<WordDefinition> Definitions { get; set; }
    }
}
