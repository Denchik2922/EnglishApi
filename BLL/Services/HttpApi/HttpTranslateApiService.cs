using BLL.Interfaces.HttpApi;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BLL.Services.HttpApi
{
    public class HttpTranslateApiService : IHttpTranslateApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiTarget;
        private readonly string _apiSource;
        public HttpTranslateApiService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            var _config = config.GetSection("TranslateApiOptions");
            _apiTarget = _config["target"];
            _apiSource = _config["source"];

        }

        public async Task<string> GetTranslatedWord(string word)
        {
            string translatedWord;
            var httpClient = _httpClientFactory.CreateClient("TranslateClient");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "q", word },
                    { "target", _apiTarget },
                    { "source", _apiSource },
                }),
            };

            using (var responce = await httpClient.SendAsync(request))
            {
                responce.EnsureSuccessStatusCode();
                var content = await responce.Content.ReadAsStringAsync();

                var Jobject = JObject.Parse(content);
                translatedWord = Jobject["data"]["translations"][0]["translatedText"].ToObject<string>();
            }
            return translatedWord;
        }



    }
}
