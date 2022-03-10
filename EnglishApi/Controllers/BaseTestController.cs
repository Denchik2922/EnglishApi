using AutoMapper;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Testing;
using EnglishApi.Dto.TestResultDtos;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.Tests;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EnglishApi.Controllers
{
    [ApiController]
    public class BaseTestController<TService, TModel> : ControllerBase where TService : class, IBaseTestService<TModel>
                                                                       where TModel : class
    {
        protected readonly TService _testingService;
        private readonly ITestResultService _testResultService;
        private readonly IMapper _mapper;
        public BaseTestController(IMapper mapper,
            TService testingService, ITestResultService testResultService)
        {
            _testResultService = testResultService;
            _testingService = testingService;
            _mapper = mapper;
        }

        [HttpGet("{Id}")]
        public virtual async Task<ActionResult> StartTest(int Id)
        {
            var test = await _testingService.StartTest(Id);
            return Ok(test);
        }

        [HttpPost]
        [Route("part-of-test")]
        public async Task<IActionResult> GetTest(TestParameters testParameters)
        {
            var test = await _testingService.GetPartOfTest(testParameters);
            return Ok(test);
        }

        [HttpPost]
        [Route("check-answer")]
        public async Task<IActionResult> CheckAnswer(ParamsForAnswer testParameters)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var test = await _testingService.GetCheckParams(testParameters, userId);

            return Ok(test);
        }

        [HttpPost]
        [Route("finish-test")]
        public async Task<IActionResult> FinishTest(TestResultDto testResult)
        {
            var test = _mapper.Map<TestResult>(testResult);
            await _testResultService.CheckUpdateOrAddResult(test);
            return Ok();
        }
    }
}
