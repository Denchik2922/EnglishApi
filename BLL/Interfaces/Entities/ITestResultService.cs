using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface ITestResultService : IBaseGenericService<TestResult>
    {
        Task CheckUpdateOrAddResult(TestResult testResult);
        Task<ICollection<TestResult>> GetAllByDictionaryIdAndUserId(int dictionaryId, string userId);
    }
}