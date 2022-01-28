using BLL.Interfaces.HttpApi;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Models.Apis;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BLL.Services.HttpApi
{
    public class HttpPhotoApiService : IHttpPhotoApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;
        private readonly string _apiPerPage;

        public HttpPhotoApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;

            var config = configuration.GetSection("PhotoApiOptions");
            _apiKey = config["Key"];
            _apiPerPage = config["Count"];
        }

        public async Task<ICollection<WordPhoto>> GetPhotosByWord(string word)
        {
            var httpClient = _httpClientFactory.CreateClient("PhotoApiClient");
            ICollection<WordPhoto> wordPhotos;
            var query = new Dictionary<string, string>
            {
                ["key"] = _apiKey,
                ["per_page"] = _apiPerPage,
                ["q"] = word
            };

            using (var responce = await httpClient.GetAsync(QueryHelpers.AddQueryString("/api/", query),
                HttpCompletionOption.ResponseHeadersRead))
            {
                responce.EnsureSuccessStatusCode();
                var content = await responce.Content.ReadAsStringAsync();
                var Jobject = JObject.Parse(content);
                wordPhotos = Jobject["hits"].ToObject<ICollection<WordPhoto>>();
            }
            return wordPhotos;
        }
    }
}
