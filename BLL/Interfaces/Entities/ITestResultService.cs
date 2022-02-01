using Models.Entities.Interfaces;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface ITestResultService<T> : IBaseGenericService<T> where T : class, IResultTest
    {
        Task<T> GetByIdAsync(string UserId, int DictionaryId);
    }
}