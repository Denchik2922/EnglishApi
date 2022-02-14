using Models.Entities;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface ITestResultService : IBaseGenericService<TestResult>
    {
        Task CheckUpdateOrAddResult(TestResult testResult);
    }
}