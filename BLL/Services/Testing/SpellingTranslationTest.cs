using BLL.Exceptions;
using BLL.Interfaces.Testing;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Tests;
using System.Linq;
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
                WordName = translate.Name
            };
            return paramQuestion;
        }

        private async Task<TranslatedWord> GetTranslate(int dicId, int currQuestion, int countWord)
        {
            var translated = await _context.TranslatedWords
                                     .Include(w => w.Word)
                                     .AsNoTracking()
                                     .Where(w => w.Word.EnglishDictionaryId == dicId)
                                     .Skip((currQuestion - 1) * countWord)
                                     .Take(countWord)
                                     .FirstOrDefaultAsync();
            if (translated == null)
            {
                throw new ItemNotFoundException($"{typeof(TranslatedWord).Name} not found in dictionary with id {dicId}");
            }
            return translated;
        }

        public override async Task<ParamsForCheck> CheckQuestion(ParamsForAnswer answerParameters)
        {
            var translatedWord = await _context.TranslatedWords
                .Include(t => t.Word)
                .FirstOrDefaultAsync(t => t.Name.ToLowerInvariant().Contains(answerParameters.Question.ToLowerInvariant()));
            if (translatedWord == null)
            {
                throw new ItemNotFoundException($"{typeof(TranslatedWord).Name} with name {answerParameters.Question} not found");
            }

            var currentWord = translatedWord.Word.Name.ToLowerInvariant();
            var paramCheck = new ParamsForCheck();
            paramCheck.Parameters = answerParameters.Parameters;

            if (currentWord.Contains(answerParameters.Answer.ToLowerInvariant()))
            {
                paramCheck.IsTrueAnswer = true;
                paramCheck.Parameters.TrueAnswers++;
            }
            else
            {
                paramCheck.TrueAnswer = currentWord;
            }

            paramCheck.Parameters.Score = (paramCheck.Parameters.TrueAnswers / paramCheck.Parameters.CountQuestion) * 100;
            return paramCheck;
        }

    }
}
