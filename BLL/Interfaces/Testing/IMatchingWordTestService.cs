using Models.Tests;
using System.Threading.Tasks;

namespace BLL.Interfaces.Testing
{
    public interface IMatchingWordTestService
    {
        Task<TestParameters> StartTest(TestParameters testParameters);
    }
}