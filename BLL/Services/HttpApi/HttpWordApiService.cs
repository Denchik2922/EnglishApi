using BLL.Interfaces.HttpApi;
using Microsoft.Extensions.Logging;
using Models.Apis;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BLL.Services.HttpApi
{
    public class HttpWordApiService : IHttpWordApiService
    {
        private const string CLIENT_NAME = "WordInfoClient";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
     
        public HttpWordApiService(IHttpClientFactory httpClientFactory, ILogger<HttpWordApiService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<WordExtraInfo> GetExtraInfoByWord(string word)
        {
            var httpClient = _httpClientFactory.CreateClient(CLIENT_NAME);

            WordExtraInfo wordInfo = new WordExtraInfo();
            ICollection<WordMeaning> wordMeanings = new List<WordMeaning>();
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
                        wordMeanings = arrObject[0]["meanings"].ToObject<ICollection<WordMeaning>>();
                    }
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, $"{ex.Message}");
            }

            List<string> wordExamples = GetWordExamples(wordMeanings);

            wordInfo.WordExamples = wordExamples;
            wordInfo.WordPhonetic = wordPhonetic;

            return wordInfo;
        }

        private List<string> GetWordExamples(ICollection<WordMeaning> wordMeanings)
        {
            return wordMeanings
                .FirstOrDefault(m => m.PartOfSpeech.Contains("verb") || m.PartOfSpeech.Contains("adjective"))
                    .Definitions
                        .Where(d => !String.IsNullOrEmpty(d.Example))
                            .Select(d => d.Example)
                            .Take(5)
                            .ToList();
        }
    }
}
