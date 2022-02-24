using AutoMapper;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Testing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Tests;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MatchingTestController : BaseTestController<IMatchingWordTest, MatchingQuestion>
    {
        public MatchingTestController(IMapper mapper,
            IMatchingWordTest matchingTest, ITestResultService testService) : base(mapper, matchingTest, testService) { }
    }
}
