using AutoMapper;
using BLL.Interfaces.Entities;
using EnglishApi.Dto.TestResultDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestResultController : ControllerBase
    {
        private readonly ITestResultService _testService;
        private readonly IMapper _mapper;
        public TestResultController(ITestResultService testService, IMapper mapper)
        {
            _testService = testService;
            _mapper = mapper;
        }

        [HttpGet("{dictionaryId}")]
        public async Task<IActionResult> GetAll(int dictionaryId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var testResults = await _testService.GetAllByDictionaryIdAndUserId(dictionaryId, userId);
            ICollection<TestResultForStatisticDto> testResultsDto = _mapper.Map<ICollection<TestResultForStatisticDto>>(testResults);

            return Ok(testResultsDto);
        }
    }
}
