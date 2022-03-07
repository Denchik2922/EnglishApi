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
    public class AudioTranslateTest: BaseTestService<AudioQuestion>, IAudioTranslateTest
    {
        public AudioTranslateTest(EnglishContext context) : base(context) { }

        public override async Task<TestParameters> StartTest(int dictionaryId, int countWord = 1)
        {
            var countQuestion = await _context.LearnedWords
                                  .Include(l => l.Word)
                                  .AsNoTracking()
                                  .Where(l => l.Word.EnglishDictionaryId == dictionaryId
                                           && !String.IsNullOrEmpty(l.Word.AudioUrl)
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

        public override async Task<AudioQuestion> GetPartOfTest(TestParameters param)
        {
            var audio = await GetAudio(param.DictionaryId, param.CurrentQuestion, param.CountWord);
            var paramQuestion = new AudioQuestion()
            {
                Parameters = param,
                AudioUrl = audio
            };
            return paramQuestion;
        }

        private async Task<string> GetAudio(int dicId, int currQuestion, int countWord)
        {
            var learnedWord = await _context.LearnedWords
                                      .Include(l => l.Word)
                                      .AsNoTracking()
                                      .Where(l => l.Word.EnglishDictionaryId == dicId 
                                                && !String.IsNullOrEmpty(l.Word.AudioUrl)
                                                && l.IsLearned == false)
                                      .Skip((currQuestion - 1) * countWord)
                                      .Take(countWord)
                                      .FirstOrDefaultAsync();
            if(learnedWord == null)
            {
                throw new ItemNotFoundException($"{typeof(LearnedWord).Name} not found");
            }

            return learnedWord.Word.AudioUrl;
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
