using Models.Entities;
using System.Collections.Generic;

namespace BLL.Tests.Infrastructure.Helpers
{
    public static class TestResultHelper
    {
        public static ResultForSpellingTest GetOne()
        {
            return new ResultForSpellingTest()
            {
                EnglishDictionaryId = 1,
                UserId = "b54e2482-5cd6-40d1-be4f-0d14e7e614e7",
                Score = 10
            };
        }

        public static IEnumerable<ResultForSpellingTest> GetMany()
        {
            yield return GetOne();
        }
    }
}
