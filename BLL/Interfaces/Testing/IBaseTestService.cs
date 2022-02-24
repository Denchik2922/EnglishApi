using Models.Tests;
using System.Threading.Tasks;

namespace BLL.Interfaces.Testing
{
    public interface IBaseTestService<T> where T : class
    {
        Task<TestParameters> StartTest(int dictionaryId, int countWord = 1);
        Task<T> GetPartOfTest(TestParameters testParameters);
        Task<ParamsForCheck> GetCheckParams(ParamsForAnswer answerParameters);
    }
}
