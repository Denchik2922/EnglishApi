using Models.Tests;
using System.Threading.Tasks;

namespace BLL.Interfaces.Testing
{
    public interface IMatchingWordTest
    {
        Task<TestParameters> StartTest(int dictionaryId);
        Task<ParamsForMatchingQuestion> GetPartOfTest(TestParameters testParameters);
        Task<ParamsForCheck> CheckQuestion(ParamsForAnswer answerParameters);
    }
}