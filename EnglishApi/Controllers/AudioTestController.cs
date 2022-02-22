using AutoMapper;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Testing;
using EnglishApi.Dto.TestResultDtos;
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
    public class AudioTestController : ControllerBase
    {
        private readonly IAudioTranslateTest _audioTest;
        private readonly ITestResultService _testService;
        private readonly IMapper _mapper;
        public AudioTestController(IMapper mapper,
            IAudioTranslateTest audioTest, ITestResultService testService)
        {
            _testService = testService;
            _audioTest = audioTest;
            _mapper = mapper;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> StartTest(int Id)
        {
            var test = await _audioTest.StartTest(Id);
            return Ok(test);
        }

        [HttpPost]
        [Route("part-of-test")]
        public async Task<IActionResult> GetTest(TestParameters testParameters)
        {
            var test = await _audioTest.GetPartOfTest(testParameters);
            return Ok(test);
        }

        [HttpPost]
        [Route("check-answer")]
        public async Task<IActionResult> CheckAnswer(ParamsForAnswer testParameters)
        {
            var test = await _audioTest.GetCheckParams(testParameters);
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
