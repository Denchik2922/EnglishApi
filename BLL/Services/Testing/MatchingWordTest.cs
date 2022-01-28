using AutoMapper;
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
    public class MatchingWordTest : IMatchingWordTest
    {
        private readonly EnglishContext _context;
        private readonly IMapper _mapper;
        public MatchingWordTest(EnglishContext context, IMapper mapper)
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
                TrueAnswers = 0,
                CountQuestion = dictionary.Words.Count,
                DictionaryId = dictionaryId,
                CurrentQuestion = 1,
                CountWord = 1
            };
        }

        public async Task<ParamsForMatchingQuestion> GetPartOfTest(TestParameters param)
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
            var translates = await _context.TranslatedWords.AsNoTracking()
                                                .Where(t => t.WordId != wordId &&
                                                            t.Word.EnglishDictionaryId == dictionaryId)
                                                .OrderBy(r => Guid.NewGuid())
                                                .Take(3)
                                                .Select(t => t.Name)
                                                .ToListAsync();

            if (translates == null)
            {
                throw new ItemNotFoundException($"{typeof(TranslatedWord).Name} not found in dictionary with id {dictionaryId}");
            }
            if (translates.Count < 3)
            {
                throw new NotEnoughItemsException($"Not enough translates in dictionary with id {dictionaryId}, must be at least 4 words");
            }

            string currentTranslate = wordTranslates.Select(t => t.Name).FirstOrDefault();
            translates.Add(currentTranslate);
            translates = translates.OrderBy(t => Guid.NewGuid()).ToList();

            return translates;
        }

        public async Task<ParamsForCheck> CheckQuestion(ParamsForAnswer answerParameters)
        {
            var word = await _context.Words
                .Include(w => w.Translates)
                .FirstOrDefaultAsync(w => w.Name.ToLower().Contains(answerParameters.Question.ToLower()));
            if (word == null)
            {
                throw new ItemNotFoundException($"{typeof(Word).Name} with name {answerParameters.Question} not found");
            }
            var currentTranslates = word.Translates.Select(t => t.Name);

            var paramCheck = new ParamsForCheck();
            paramCheck.Parameters = answerParameters.Parameters;

            if (currentTranslates.Contains(answerParameters.Answer))
            {
                paramCheck.IsTrueAnswer = true;
                paramCheck.Parameters.TrueAnswers++;

            }
            paramCheck.Parameters.Score = (paramCheck.Parameters.TrueAnswers / paramCheck.Parameters.CountQuestion) * 100;
            paramCheck.TrueAnswer = currentTranslates.First();
            return paramCheck;
        }
    }
}
