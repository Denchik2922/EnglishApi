using BLL.Interfaces.HttpApi;
using Models.Apis;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BLL.Services.HttpApi
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
                try
                {
                    responce.EnsureSuccessStatusCode();
                }
                catch(HttpRequestException e)
                {
                    throw;
                }
                var content = await responce.Content.ReadAsStringAsync();
                
                var arrObject = JArray.Parse(content);
                wordPhonetic = arrObject[0]["phonetics"][0].ToObject<WordPhonetic>();
            }
            return wordPhonetic;
        }

    }
}
