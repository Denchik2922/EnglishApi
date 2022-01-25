using AutoMapper;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Testing;
using EnglishApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Tests;
using System.Threading.Tasks;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MatchingTestController : ControllerBase
    {
        private readonly IMatchingWordTest _matchingTest;
        
        private readonly IMapper _mapper;
        private readonly ITestResultService<ResultForMatchingTest> _testService;
        public MatchingTestController(IMapper mapper,
            IMatchingWordTest matchingTest, ITestResultService<ResultForMatchingTest> testService)
        {
            _matchingTest = matchingTest;
            _testService = testService;
            _mapper = mapper;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> StartTest(int Id)
        {
            var test = await _matchingTest.StartTest(Id);
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
            var test = await _matchingTest.CheckQuestion(testParameters);
            return Ok(test);
        }

        [HttpPost]
        [Route("finish-test")]
        public async Task<IActionResult> FinishTest(TestResultDto testResult)
        {
            var test = _mapper.Map<ResultForMatchingTest>(testResult);
            var result = await _testService.GetByIdAsync(test.UserId, test.EnglishDictionaryId);
            if(result == null)
            {
                await _testService.AddAsync(test);
            }
            else
            {
                result.Score = test.Score;
                await _testService.UpdateAsync(result);
            }
            return Ok();
        }
    }
}
