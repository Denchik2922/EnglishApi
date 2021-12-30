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
    public class TestController : ControllerBase
    {
        private readonly IMatchingWordTestService _wordTestService;
        private readonly IBaseGenaricService<TestResult> _testService;
        private readonly IMapper _mapper;
        public TestController(IMapper mapper, IMatchingWordTestService wordTestService, IBaseGenaricService<TestResult> testService)
        {
            _wordTestService = wordTestService;
            _testService = testService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("start-test")]
        public async Task<IActionResult> Start(TestParameters testParameters)
        {
            TestParameters test = await _wordTestService.StartTest(testParameters);

            var metadata = new
            {
                test.DictionaryId,
                test.CountWord,
                test.HasNextQuestion,
                test.CurrentQuestion,
                test.NextQuestion,
                test.Score,
                test.CountQuestion
            };
            Response.Headers.Add("Test-Info", JsonConvert.SerializeObject(metadata));

            return Ok(test.WordForTest);
        }

        [HttpPost]
        [Route("finish-test")]
        public async Task<IActionResult> Finish(TestResultDto testResult)
        {
            TestResult test = _mapper.Map<TestResult>(testResult);
            await _testService.AddAsync(test);
            return Ok();
        }
    }
}
