using Models.Entities;
using System.Collections.Generic;

namespace BLL.Tests.Infrastructure.Helpers
{
    public static class EnglishDictionaryTagHelper
    {
        public static EnglishDictionaryTag GetOne()
        {
            return new EnglishDictionaryTag()
            {
                EnglishDictionaryId = 1,
                TagId = 1,
            };
        }

        public static IEnumerable<EnglishDictionaryTag> GetMany()
        {
            yield return GetOne();
        }
    }
}
