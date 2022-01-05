using Models.Entities;
using System.Collections.Generic;

namespace BLL.Tests.Infrastructure.Helpers
{
    public static class WordHelper
    {
        public static Word GetOne(int id)
        {
            return new Word()
            {
                Id = id,
                Name = $"Word {id}",
                EnglishDictionaryId = 1,
                Transcription = $"Word transcription {id}",
                PictureUrl = $"Word picture {id}",
                AudioUrl = $"Word audio {id}"
            };
        }

        public static IEnumerable<Word> GetMany()
        {
            yield return GetOne(1);
            yield return GetOne(2);
        }
    }
}
