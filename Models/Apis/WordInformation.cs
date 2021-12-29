using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Apis
{
    public class WordInformation
    {
        public string Translate { get; set; }
        public string Transcription { get; set; }
        public ICollection<WordPhoto> PictureUrls { get; set; }
        public string AudioUrl { get; set; }
    }
}
