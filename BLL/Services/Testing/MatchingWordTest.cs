using BLL.Interfaces.Testing;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Tests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Testing
{
    public class MatchingWordTest : IMatchingWordTest
    {
        private readonly EnglishContext _context;
        public MatchingWordTest(EnglishContext context)
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

            var translates = await _context.TranslatedWords.AsNoTracking()
                                                .Where(t => t.WordId != word.Id)
                                                .OrderBy(r => Guid.NewGuid())
                                                .Take(3)
                                                .Select(t => t.Name)
                                                .ToListAsync();

            string currentTranslate = word.Translates.Select(t => t.Name).FirstOrDefault();
            translates.Add(currentTranslate);

            var wordForTest = new Models.Tests.MatchingWordTest()
            {
                WordName = word.Name,
                CorrectAnswer = currentTranslate,
                Translates = translates
            };

            return wordForTest;
        }
    }
}
