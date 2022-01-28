using BLL.Exceptions;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Tests;
using System.Threading.Tasks;

namespace BLL.Services.Testing
{
    public abstract class BaseTestService<T>
    {
        protected readonly EnglishContext _context;
        public BaseTestService(EnglishContext context)
        {
            _context = context;
        }

        public async Task<TestParameters> StartTest(int dictionaryId)
        {
            var dictionary = await _context.EnglishDictionaries
                                            .Include(d => d.Words)
                                            .FirstOrDefaultAsync(d => d.Id == dictionaryId);
            if (dictionary == null)
            {
                throw new ItemNotFoundException($"{typeof(EnglishDictionary).Name} with id {dictionaryId} not found");
            }
            return new TestParameters()
            {
                Score = 0,
                TrueAnswers = 0,
                CountQuestion = dictionary.Words.Count,
                DictionaryId = dictionaryId,
                CurrentQuestion = 1,
                CountWord = 1
            };
        }

        public abstract Task<T> GetPartOfTest(TestParameters param);
        public abstract Task<ParamsForCheck> CheckQuestion(ParamsForAnswer answerParameters);
    }
}
