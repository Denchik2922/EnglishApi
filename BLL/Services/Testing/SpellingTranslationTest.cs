using BLL.Interfaces.Testing;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Tests;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Testing
{
    public class SpellingTranslationTest : ISpellingTranslationTest
    {
        private readonly EnglishContext _context;
        public SpellingTranslationTest(EnglishContext context)
        {
            _context = context;
        }

        public async Task<IWordTest> GetPartOfTest(TestParameters testParameters)
        {
            var word = await _context.Words
                                     .Include(w => w.Translates)
                                     .AsNoTracking()
                                     .Where(w => w.EnglishDictionaryId == testParameters.DictionaryId)
                                     .Skip((testParameters.CurrentQuestion - 1) * testParameters.CountWord)
                                     .Take(testParameters.CountWord)
                                     .FirstOrDefaultAsync();

            string currentTranslate = word.Translates.Select(t => t.Name).FirstOrDefault();

            TranslateWordTest wordForTest = new TranslateWordTest()
            {
                WordName = word.Name,
                CorrectAnswer = currentTranslate
            };

            return wordForTest;
        }
    }
}
