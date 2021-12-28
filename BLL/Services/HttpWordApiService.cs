using BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using Models.Apis;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class HttpWordApiService : IHttpWordApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HttpWordApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
      
        public async Task<WordPhonetic> GetPhoneticByWord(string word)
        {
            var httpClient = _httpClientFactory.CreateClient("WordInfoClient");
            WordPhonetic wordPhonetic;

            using (var responce = await httpClient.GetAsync(word,
                HttpCompletionOption.ResponseHeadersRead))
            {
                responce.EnsureSuccessStatusCode();
                var content = (await responce.Content.ReadAsStringAsync()).TrimStart('[').TrimEnd(']');
                
                var Jobject = JObject.Parse(content);
                wordPhonetic = Jobject["phonetics"][0].ToObject<WordPhonetic>();
            }
            return wordPhonetic;
        }

    }
}
