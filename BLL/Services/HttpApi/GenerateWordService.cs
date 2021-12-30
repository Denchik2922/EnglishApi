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
        public GenerateWordService(IHttpPhotoApiService photoApi, IHttpTranslateApiService translateApi, IHttpWordApiService wordApi)
        {
            _photoApi = photoApi;
            _translateApi = translateApi;
            _wordApi = wordApi;
        }

        public async Task<WordInformation> GenerateInfoByWord(string wordName)
        {
            var phonetic = await _wordApi.GetPhoneticByWord(wordName);
            return new WordInformation()
            {
                PictureUrls = await _photoApi.GetPhotosByWord(wordName),
                Translate = await _translateApi.GetTranslatedWord(wordName),
                Transcription = phonetic.Text,
                AudioUrl = phonetic.Audio
            };
        }
    }
}
