using AutoMapper;
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
    public class SpellingTranslationTest : ISpellingTranslationTest
    {
        private readonly EnglishContext _context;
        private readonly IMapper _mapper;
        public SpellingTranslationTest(EnglishContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                CountQuestion = dictionary.Words.Count,
                DictionaryId = dictionaryId,
                CurrentQuestion = 1,
                CountWord = 1
            };
        }

        public async Task<ParamsForTranslateQuestion> GetPartOfTest(TestParameters param)
        {
            var translate = await GetTranslate(param.DictionaryId, param.CurrentQuestion, param.CountWord);
            
            TranslateQuestion wordForTest = new TranslateQuestion()
            {
                WordName = translate.Name,
            };

            var paramQuestion = new ParamsForTranslateQuestion()
            {
                Question = wordForTest
            };

            _mapper.Map(param, paramQuestion);

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

        public async Task<ParamsForCheck> CheckQuestion(ParamsForAnswer answerParameters)
        {
            var translatedWord = await _context.TranslatedWords
                .Include(t => t.Word)
                .FirstOrDefaultAsync(t => t.Name.ToLower().Contains(answerParameters.UserAnswer.Question));
            if (translatedWord == null)
            {
                throw new ItemNotFoundException($"{typeof(TranslatedWord).Name} with name {answerParameters.UserAnswer.Question} not found");
            }
            var currentWord = translatedWord.Word.Name;

            var paramCheck = new ParamsForCheck();
            _mapper.Map(answerParameters, paramCheck);

            if (currentWord.Contains(answerParameters.UserAnswer.Answer))
            {
                paramCheck.IsTrueAnswer = true;
                paramCheck.Score++;
                paramCheck.TrueAnswer = currentWord;
             } 
            paramCheck.TrueAnswer = currentWord;
            return paramCheck;
        }

    }
}
