using Models.Tests;
using System.Threading.Tasks;

namespace BLL.Interfaces.Testing
{
    public interface ISpellingTranslationTest
    {
        Task<IWordTest> GetPartOfTest(TestParameters testParameters);
    }
}