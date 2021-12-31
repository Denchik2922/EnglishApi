using Models.Tests;
using System.Threading.Tasks;

namespace BLL.Interfaces.Testing
{
    public interface IMatchingWordTest
    {
        Task<IWordTest> GetPartOfTest(TestParameters testParameters);
    }
}