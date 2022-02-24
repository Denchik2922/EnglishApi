using AutoMapper;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Testing;
using EnglishApi.Dto.TestResultDtos;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Tests;
using System.Threading.Tasks;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultipleMatchingTestController : ControllerBase
    {
        private readonly IMultipleWordMatchingTest _matchingTest;
        private readonly ITestResultService _testService;
        private readonly IMapper _mapper;
        public MultipleMatchingTestController(IMapper mapper,
             IMultipleWordMatchingTest matchingTest, ITestResultService testService)
        {
            _testService = testService;
            _matchingTest = matchingTest;
            _mapper = mapper;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> StartTest(int Id)
        {
            var test = await _matchingTest.StartTest(Id, 4);
            return Ok(test);
        }

        [HttpPost]
        [Route("part-of-test")]
        public async Task<IActionResult> GetTest(TestParameters testParameters)
        {
            var test = await _matchingTest.GetPartOfTest(testParameters);
            return Ok(test);
        }

        [HttpPost]
        [Route("check-answer")]
        public async Task<IActionResult> CheckAnswer(ParamsForAnswer testParameters)
        {
            var test = await _matchingTest.GetCheckParams(testParameters);
            return Ok(test);
        }

        [HttpPost]
        [Route("finish-test")]
        public async Task<IActionResult> FinishTest(TestResultDto testResult)
        {
            var test = _mapper.Map<TestResult>(testResult);
            await _testService.CheckUpdateOrAddResult(test);
            return Ok();
        }
    }
}
