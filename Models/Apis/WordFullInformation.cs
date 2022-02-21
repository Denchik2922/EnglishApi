using System.Collections.Generic;

namespace Models.Apis
{
    public class WordFullInformation
    {
        public string Translate { get; set; }
        public string Transcription { get; set; }
        public ICollection<WordPhoto> PictureUrls { get; set; }
        public string AudioUrl { get; set; }
        public ICollection<string> WordExamples { get; set; }
    }
}
