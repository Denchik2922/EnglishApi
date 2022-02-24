using BLL.Exceptions;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Tests;
using System;
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

        public virtual async Task<TestParameters> StartTest(int dictionaryId, int countWord = 1)
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
                CountQuestion = dictionary.Words.Count ,
                DictionaryId = dictionaryId,
                CurrentQuestion = 1,
                CountWord = countWord
            };
        }

        protected double GetCalculateScore(int CountTrueAnswers, int CountQustion)
        {
            double score = ((double)CountTrueAnswers / CountQustion) * 100;
            return Math.Round(score, 2);
        }

        public abstract Task<T> GetPartOfTest(TestParameters param);
        public abstract Task<ParamsForCheck> GetCheckParams(ParamsForAnswer answerParameters);
    }
}
