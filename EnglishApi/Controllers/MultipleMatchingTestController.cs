using AutoMapper;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Testing;
using Microsoft.AspNetCore.Mvc;
using Models.Tests;
using System.Threading.Tasks;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultipleMatchingTestController : BaseTestController<IMultipleWordMatchingTest, MultipleMatchingQuestion>
    {
        public MultipleMatchingTestController(IMapper mapper,
           IMultipleWordMatchingTest multipeTest, ITestResultService testService) : base(mapper, multipeTest, testService) { }

        [HttpGet("{Id}")]
        public override async Task<ActionResult> StartTest(int Id)
        {
            var test = await _testingService.StartTest(Id, 4);
            return Ok(test);
        }

    }
}
