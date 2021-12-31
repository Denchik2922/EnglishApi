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
        private readonly IBaseGenaricService<TestResult> _testService;
        private readonly IMapper _mapper;
        public SpellingTestController(IMapper mapper,
            ISpellingTranslationTest spellingTest, IBaseGenaricService<TestResult> testService)
        {
            _spellingTest = spellingTest;
            _testService = testService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("part-of-test")]
        public async Task<IActionResult> GetTest(TestParameters testParameters)
        {
            var test = await _spellingTest.GetPartOfTest(testParameters);
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
