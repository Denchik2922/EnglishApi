using BLL.Exceptions;
using BLL.Interfaces.Testing;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Services.Testing
{
    public class AudioTranslateTest: BaseTestService<ParamsForAudioQuestion>, IAudioTranslateTest
    {
        public AudioTranslateTest(EnglishContext context) : base(context) { }

        public override async Task<TestParameters> StartTest(int dictionaryId)
        {
            var dictionary = await _context.EnglishDictionaries
                                            .Include(d => d.Words)
                                            .FirstOrDefaultAsync(d => d.Id == dictionaryId);
            if (dictionary == null)
            {
                throw new ItemNotFoundException($"{typeof(EnglishDictionary).Name} with id {dictionaryId} not found");
            }

            var countWord = dictionary.Words.Count(w => !String.IsNullOrEmpty(w.AudioUrl));

            return new TestParameters()
            {
                Score = 0,
                TrueAnswers = 0,
                CountQuestion = countWord,
                DictionaryId = dictionaryId,
                CurrentQuestion = 1,
                CountWord = 1
            };
        }

        public override async Task<ParamsForAudioQuestion> GetPartOfTest(TestParameters param)
        {
            var audio = await GetAudio(param.DictionaryId, param.CurrentQuestion, param.CountWord);
            var paramQuestion = new ParamsForAudioQuestion()
            {
                Parameters = param,
                AudioUrl = audio
            };
            return paramQuestion;
        }

        private async Task<string> GetAudio(int dicId, int currQuestion, int countWord)
        {
            var word = await _context.Words
                                     .AsNoTracking()
                                     .Where(w => w.EnglishDictionaryId == dicId && !String.IsNullOrEmpty(w.AudioUrl))
                                     .Skip((currQuestion - 1) * countWord)
                                     .Take(countWord)
                                     .FirstOrDefaultAsync();
            return word.AudioUrl;
        }

        public override async Task<ParamsForCheck> GetCheckParams(ParamsForAnswer answerParameters)
        {
            var word = await _context.Words
                                    .Include(w => w.Translates)
                                    .FirstOrDefaultAsync(w => w.AudioUrl.Contains(answerParameters.Question));

            if (word == null)
            {
                throw new ItemNotFoundException($"{typeof(Word).Name} not found");
            }

            var currentTranslates = word.Translates.Select(t => t.Name.ToLowerInvariant()).ToList();

            var userAnswer = Regex.Replace(answerParameters.Answer.Trim().ToLowerInvariant(), " {1,}", " ");

            var paramCheck = new ParamsForCheck();
            paramCheck.Parameters = answerParameters.Parameters;

            CheckQuestion(paramCheck, currentTranslates, userAnswer);

            return paramCheck;
        }

        private void CheckQuestion(ParamsForCheck paramCheck, List<string> currentTranslates, string userAnswer)
        {
            if (currentTranslates.Contains(userAnswer) && !String.IsNullOrEmpty(userAnswer))
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
