using BLL.Exceptions;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Tests;
using System;
using System.Linq;
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
            var countQuestion = await _context.LearnedWords
                                   .Include(l => l.Word)
                                   .AsNoTracking()
                                   .Where(l => l.Word.EnglishDictionaryId == dictionaryId 
                                            && l.IsLearned == false)
                                   .CountAsync();

            return new TestParameters()
            {
                Score = 0,
                TrueAnswers = 0,
                CountQuestion = countQuestion,
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
