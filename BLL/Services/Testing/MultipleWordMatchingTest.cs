using BLL.Exceptions;
using BLL.Interfaces.Testing;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Testing
{
    public class MultipleWordMatchingTest : BaseTestService<MultipleMatchingQuestion>, IMultipleWordMatchingTest
    {
        public MultipleWordMatchingTest(EnglishContext context) : base(context) { }

        public override async Task<TestParameters> StartTest(int dictionaryId, int countWord = 1)
        {
            var dictionary = await _context.EnglishDictionaries
                                            .Include(d => d.Words)
                                            .FirstOrDefaultAsync(d => d.Id == dictionaryId);
            if (dictionary == null)
            {
                throw new ItemNotFoundException($"{typeof(EnglishDictionary).Name} with id {dictionaryId} not found");
            }

            double count = dictionary.Words.Count / (double)countWord;
            var countQuestion = (int)Math.Ceiling(count);

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

        public override async Task<MultipleMatchingQuestion> GetPartOfTest(TestParameters param)
        {
            var words = await GetWords(param.DictionaryId, param.CurrentQuestion, param.CountWord);

            var wordNames = words.Select(word => word.Name)
                                 .OrderBy(t => Guid.NewGuid())
                                 .ToList();

            var wordTranslates = words.Select(w => String.Join(", ", w.Translates.Select(t => t.Name)))
                                      .OrderBy(t => Guid.NewGuid())
                                      .ToList();

            var paramQuestion = new MultipleMatchingQuestion()
            {
                Parameters = param,
                WordNames = wordNames,
                Translates = wordTranslates
            };

            return paramQuestion;
        }

        private async Task<ICollection<Word>> GetWords(int dicId, int currQuestion, int countWord)
        {
            var words = await _context.Words
                                     .Include(w => w.Translates)
                                     .AsNoTracking()
                                     .Where(w => w.EnglishDictionaryId == dicId)
                                     .Skip((currQuestion - 1) * countWord)
                                     .Take(countWord)
                                     .ToListAsync();
            if (words == null)
            {
                throw new ItemNotFoundException($"{typeof(Word).Name} not found in dictionary with id {dicId}");
            }

            return words;
        }

        public override async Task<ParamsForCheck> GetCheckParams(ParamsForAnswer answerParameters)
        {
            var word = await _context.Words
                .Include(w => w.Translates)
                .FirstOrDefaultAsync(w => w.Name.ToLower().Contains(answerParameters.Question.ToLowerInvariant()));

            if (word == null)
            {
                throw new ItemNotFoundException($"{typeof(Word).Name} with name {answerParameters.Question} not found");
            }

            var currentTranslates = word.Translates.Select(t => t.Name).ToList();

            var paramCheck = new ParamsForCheck();
            paramCheck.Parameters = answerParameters.Parameters;

            var answerTranslates = answerParameters.Answer.Split(',').ToList();

            await CheckQuestion(paramCheck, currentTranslates, answerTranslates);

            return paramCheck;
        }

        private async Task CheckQuestion(ParamsForCheck paramCheck, List<string> currentTranslates, List<string> answerTranslates)
        {
            if (currentTranslates.Any(t => answerTranslates.Contains(t)))
            {
                paramCheck.IsTrueAnswer = true;
                paramCheck.Parameters.TrueAnswers++;
            }

            var dictionary = await _context.EnglishDictionaries
                                            .Include(d => d.Words)
                                            .FirstOrDefaultAsync(d => d.Id == paramCheck.Parameters.DictionaryId);

            var countQuestion = dictionary.Words.Count;

            paramCheck.Parameters.Score = GetCalculateScore(paramCheck.Parameters.TrueAnswers, countQuestion);
        }
    }
}
