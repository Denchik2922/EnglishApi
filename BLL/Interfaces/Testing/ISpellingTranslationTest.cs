using Models.Tests;
using System.Threading.Tasks;

namespace BLL.Interfaces.Testing
{
    public interface ISpellingTranslationTest
    {
        Task<TestParameters> StartTest(int dictionaryId);
        Task<ParamsForTranslateQuestion> GetPartOfTest(TestParameters testParameters);
        Task<ParamsForCheck> CheckQuestion(ParamsForAnswer answerParameters);
    }
}