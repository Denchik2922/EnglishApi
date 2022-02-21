using BLL.Interfaces.HttpApi;
using Models.Apis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.HttpApi
{
    public class GenerateWordService : IGenerateWordService
    {
        private readonly IHttpPhotoApiService _photoApi;
        private readonly IHttpTranslateApiService _translateApi;
        private readonly IHttpWordApiService _wordApi;
        public GenerateWordService(IHttpPhotoApiService photoApi,
                                   IHttpTranslateApiService translateApi,
                                   IHttpWordApiService wordApi)
        {
            _photoApi = photoApi;
            _translateApi = translateApi;
            _wordApi = wordApi;
        }

        public async Task<WordFullInformation> GenerateInfoByWord(string wordName)
        {
            ICollection<WordPhoto> photos = await _photoApi.GetPhotosByWord(wordName);
            string translate = await _translateApi.GetTranslatedWord(wordName);

            var extraInfo = await _wordApi.GetExtraInfoByWord(wordName);
            WordPhonetic phonetic = extraInfo.WordPhonetic;

            return new WordFullInformation()
            {
                PictureUrls = photos,
                Translate = translate,
                Transcription = phonetic.Text,
                AudioUrl = phonetic.Audio,
                WordExamples = extraInfo.WordExamples
            };
        }
    }
}
