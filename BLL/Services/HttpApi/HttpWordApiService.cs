using BLL.Interfaces.HttpApi;
using Microsoft.Extensions.Logging;
using Models.Apis;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BLL.Services.HttpApi
{
    public class HttpWordApiService : IHttpWordApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        public HttpWordApiService(IHttpClientFactory httpClientFactory, ILogger<HttpWordApiService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<WordPhonetic> GetPhoneticByWord(string word)
        {
            var httpClient = _httpClientFactory.CreateClient("WordInfoClient");
            WordPhonetic wordPhonetic = new WordPhonetic();

            try
            {
                using (var response = await httpClient.GetAsync(word,
                HttpCompletionOption.ResponseHeadersRead))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        var arrObject = JArray.Parse(content);
                        wordPhonetic = arrObject[0]["phonetics"][0].ToObject<WordPhonetic>();
                    }
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, $"{ex.Message}");
            }
            return wordPhonetic;

        }

    }
}
