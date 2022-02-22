using BLL.Exceptions;
using BLL.Interfaces.Testing;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Tests;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Services.Testing
{
    public class SpellingTranslationTest : BaseTestService<ParamsForTranslateQuestion>, ISpellingTranslationTest
    {
        public SpellingTranslationTest(EnglishContext context): base(context){}

        public override async Task<ParamsForTranslateQuestion> GetPartOfTest(TestParameters param)
        {
            var translate = await GetTranslate(param.DictionaryId, param.CurrentQuestion, param.CountWord);
            var paramQuestion = new ParamsForTranslateQuestion()
            {
                Parameters = param,
                WordName = translate
            };
            return paramQuestion;
        }

        private async Task<string> GetTranslate(int dicId, int currQuestion, int countWord)
        {
            var wordTranslates = await _context.Words
                                     .Include(w => w.Translates)
                                     .AsNoTracking()
                                     .Where(w => w.EnglishDictionaryId == dicId)
                                     .Skip((currQuestion - 1) * countWord)
                                     .Take(countWord)
                                     .Select(w => String.Join(", ", w.Translates.Select(t => t.Name)))
                                     .FirstOrDefaultAsync();
            return wordTranslates;
        }

        public override async Task<ParamsForCheck> GetCheckParams(ParamsForAnswer answerParameters)
        {
            var questionTranslates = answerParameters.Question.Split(',');

            var word = await _context.Words
                                    .Include(w => w.Translates)
                                    .FirstOrDefaultAsync(w => w.Translates
                                                               .Any(t => questionTranslates.Contains(t.Name)));
            if (word == null)
            {
                throw new ItemNotFoundException($"{typeof(TranslatedWord).Name} with name {answerParameters.Question} not found");
            }
            
            var userAnswer = Regex.Replace(answerParameters.Answer.Trim().ToLowerInvariant(), " {1,}", " " );

            var paramCheck = new ParamsForCheck();
            paramCheck.Parameters = answerParameters.Parameters;

            CheckQuestion(paramCheck, word, userAnswer);

            return paramCheck;
        }

        private void CheckQuestion(ParamsForCheck paramCheck, Word word, string userAnswer)
        {
            var currentWord = word.Name.ToLowerInvariant();

            if (currentWord.Contains(userAnswer) && !String.IsNullOrEmpty(userAnswer))
            {
                paramCheck.IsTrueAnswer = true;
                paramCheck.Parameters.TrueAnswers++;
            }
            else
            {
                paramCheck.TrueAnswer = word.Name;
            }

            paramCheck.Parameters.Score = GetCalculateScore(paramCheck.Parameters.TrueAnswers, paramCheck.Parameters.CountQuestion);
        }
    }
}
