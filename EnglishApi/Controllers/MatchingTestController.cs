using AutoMapper;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Testing;
using EnglishApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Tests;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchingTestController : ControllerBase
    {
        private readonly IMatchingWordTest _matchingTest;
        private readonly IBaseGenaricService<TestResult> _testService;
        private readonly IMapper _mapper;
        public MatchingTestController(IMapper mapper,
            IMatchingWordTest matchingTest, IBaseGenaricService<TestResult> testService)
        {
            _matchingTest = matchingTest;
            _testService = testService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("part-of-test")]
        public async Task<IActionResult> GetTest(TestParameters testParameters)
        {
            var test = await _matchingTest.GetPartOfTest(testParameters);
            Response.Headers.Add("Test-Info", JsonConvert.SerializeObject(testParameters));
            return Ok(test);
        }



        [HttpPost]
        [Route("finish-test")]
        public async Task<IActionResult> FinishTest(TestResultDto testResult)
        {
            TestResult test = _mapper.Map<TestResult>(testResult);
            await _testService.AddAsync(test);
            return Ok();
        }
    }
}
