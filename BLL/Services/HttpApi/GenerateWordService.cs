using BLL.Interfaces.HttpApi;
using Models.Apis;

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

        public async Task<WordInformation> GenerateInfoByWord(string wordName)
        {
            var phonetic = await _wordApi.GetPhoneticByWord(wordName);
            var photos = await _photoApi.GetPhotosByWord(wordName);
            var translate = await _translateApi.GetTranslatedWord(wordName);

            return new WordInformation()
            {
                PictureUrls = photos,
                Translate = translate,
                Transcription = phonetic.Text,
                AudioUrl = phonetic.Audio
            };
        }
    }
}
