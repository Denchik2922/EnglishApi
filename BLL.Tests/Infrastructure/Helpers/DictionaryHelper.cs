using Models.Entities;
using System.Collections.Generic;

namespace BLL.Tests.Infrastructure.Helpers
{
    public static class DictionaryHelper
    {
        public static EnglishDictionary GetOne(int id)
        {
            return new EnglishDictionary()
            {
                Id = id,
                Name = $"Dictionary {id}",
                Description = $"Dictionary description {id}",
                UserId = "b54e2482-5cd6-40d1-be4f-0d14e7e614e7",
                IsPrivate = true,

            };
        }

        public static IEnumerable<EnglishDictionary> GetMany()
        {
            yield return GetOne(1);
        }
    }
}
