using BLL.Interfaces.Entities;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities.Interfaces;
using System.Threading.Tasks;

namespace BLL.Services.Entities
{
    public class TestResultService<T> : BaseGenericService<T>, ITestResultService<T> where T : class, IResultTest
    {
        public TestResultService(EnglishContext context) : base(context) { }

        public async Task<T> GetByIdAsync(string UserId, int DictionaryId)
        {
            return await _dbSet.FirstOrDefaultAsync(r => r.UserId == UserId && r.EnglishDictionaryId == DictionaryId);
        }
    }
}
