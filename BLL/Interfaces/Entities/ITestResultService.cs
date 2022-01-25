using Models.Entities;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface ITestResultService<T> : IBaseGenaricService<T> where T : class, IResultTest
    {
        Task<T> GetByIdAsync(string UserId, int DictionaryId);
    }
}