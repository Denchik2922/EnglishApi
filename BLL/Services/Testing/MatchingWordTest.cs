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
    public class MatchingWordTest : BaseTestService<ParamsForMatchingQuestion>, IMatchingWordTest
    {
        public MatchingWordTest(EnglishContext context) : base(context){}

        public override async Task<ParamsForMatchingQuestion> GetPartOfTest(TestParameters param)
        {
            var word = await GetWord(param.DictionaryId, param.CurrentQuestion, param.CountWord);
            var translates = await GetTranslates(word.Id, param.DictionaryId, word.Translates);

            var paramQuestion = new ParamsForMatchingQuestion()
            {
                Parameters = param,
                WordName = word.Name,
                Translates = translates
            };

            return paramQuestion;
        }

        private async Task<Word> GetWord(int dicId, int currQuestion, int countWord)
        {
            var word = await _context.Words
                                     .Include(w => w.Translates)
                                     .AsNoTracking()
                                     .Where(w => w.EnglishDictionaryId == dicId)
                                     .Skip((currQuestion - 1) * countWord)
                                     .Take(countWord)
                                     .FirstOrDefaultAsync();
            if (word == null)
            {
                throw new ItemNotFoundException($"{typeof(Word).Name} not found in dictionary with id {dicId}");
            }
            return word;
        }

        private async Task<ICollection<string>> GetTranslates(int wordId,
                                                              int dictionaryId,
                                                              ICollection<TranslatedWord> wordTranslates)
        {
            var translates = await _context.Words.AsNoTracking()
                                                .Where(w => w.Id != wordId &&
                                                            w.EnglishDictionaryId == dictionaryId)
                                                .OrderBy(r => Guid.NewGuid())
                                                .Take(3)
                                                .Select(w => String.Join(", ", w.Translates.Select(t => t.Name)))
                                                .ToListAsync();

            if (translates == null)
            {
                throw new ItemNotFoundException($"{typeof(TranslatedWord).Name} not found in dictionary with id {dictionaryId}");
            }
            if (translates.Count < 3)
            {
                throw new NotEnoughItemsException($"Not enough translates in dictionary with id {dictionaryId}, must be at least 4 words");
            }

            string currentTranslate = String.Join(", ", wordTranslates.Select(t => t.Name));

            translates.Add(currentTranslate);
            translates = translates.OrderBy(t => Guid.NewGuid()).ToList();

            return translates;
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

            CheckQuestion(paramCheck, currentTranslates, answerTranslates);

            return paramCheck;
        }

        private void CheckQuestion(ParamsForCheck paramCheck, List<string> currentTranslates, List<string> answerTranslates)
        {
            if (currentTranslates.Any(t => answerTranslates.Contains(t)))
            {
                paramCheck.IsTrueAnswer = true;
                paramCheck.Parameters.TrueAnswers++;
            }
            else
            {
                paramCheck.TrueAnswer = String.Join(", ", currentTranslates);
            }

            paramCheck.Parameters.Score = GetCalculateScore(paramCheck.Parameters.TrueAnswers, paramCheck.Parameters.CountQuestion);
        }
    }
}
