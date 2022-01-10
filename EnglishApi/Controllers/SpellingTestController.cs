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
    public class SpellingTestController : ControllerBase
    {
        private readonly ISpellingTranslationTest _spellingTest;
        private readonly IBaseGenaricService<ResultForSpellingTest> _testService;
        private readonly IMapper _mapper;
        public SpellingTestController(IMapper mapper,
            ISpellingTranslationTest spellingTest, IBaseGenaricService<ResultForSpellingTest> testService)
        {
            _spellingTest = spellingTest;
            _testService = testService;
            _mapper = mapper;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> StartTest(int Id)
        {
            var test = await _spellingTest.StartTest(Id);
            return Ok(test);
        }

        [HttpPost]
        [Route("part-of-test")]
        public async Task<IActionResult> GetTest(TestParameters testParameters)
        {
            var test = await _spellingTest.GetPartOfTest(testParameters);
            return Ok(test);
        }

        [HttpPost]
        [Route("check-answer")]
        public async Task<IActionResult> CheckAnswer(ParamsForAnswer testParameters)
        {
            var test = await _spellingTest.CheckQuestion(testParameters);
            return Ok(test);
        }



        [HttpPost]
        [Route("finish-test")]
        public async Task<IActionResult> FinishTest(TestResultDto testResult)
        {
            var test = _mapper.Map<ResultForSpellingTest>(testResult);
            await _testService.AddAsync(test);
            return Ok();
        }
    }
}
