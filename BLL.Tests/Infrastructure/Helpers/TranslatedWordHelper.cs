using Models.Entities;
using System.Collections.Generic;

namespace BLL.Tests.Infrastructure.Helpers
{
    public static class TranslatedWordHelper
    {
        public static TranslatedWord GetOne(int id)
        {
            return new TranslatedWord()
            {
                Id = id,
                Name = $"TranslatedWord {id}",
                WordId = 1,
            };
        }

        public static IEnumerable<TranslatedWord> GetMany()
        {
            yield return GetOne(1);
        }
    }
}
