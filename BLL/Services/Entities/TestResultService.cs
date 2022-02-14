using AutoMapper;
using BLL.Interfaces.Entities;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Entities
{
    public class TestResultService : BaseGenericService<TestResult>, ITestResultService
    {
        public TestResultService(EnglishContext context) : base(context) { }

        public async Task CheckUpdateOrAddResult(TestResult testResult)
        {
            var results = await _context.TestResults
                                        .Where(t => t.UserId == testResult.UserId && t.TypeOfTestingId == testResult.TypeOfTestingId)
                                        .ToListAsync();

            var resultToday = results.FirstOrDefault(r => r.Date.Date == testResult.Date.Date);

            if (resultToday == null)
            {
                await AddAsync(testResult);
            }
            else if (resultToday.Score < testResult.Score)
            {
                resultToday.Score = testResult.Score;
                await UpdateAsync(resultToday);
            }
        }
    }
}
