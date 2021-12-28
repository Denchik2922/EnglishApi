using BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class HttpTranslateApiService : IHttpTranslateApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HttpTranslateApiService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetTranslatedWord(string word)
        {
            var httpClient = _httpClientFactory.CreateClient("TranslateClient");
            var request = new HttpRequestMessage
            {

                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "q", "Hello, world!" },
                    { "target", "es" },
                    { "source", "en" },
                }),
            };

            string translatedWord;

            using (var responce = await httpClient.SendAsync(request))
            {
                responce.EnsureSuccessStatusCode();
                var content = await responce.Content.ReadAsStringAsync();

                var Jobject = JObject.Parse(content);
                translatedWord = Jobject["phonetics"][0].ToObject<string>();
            }
            return translatedWord;
        }



    }
}
