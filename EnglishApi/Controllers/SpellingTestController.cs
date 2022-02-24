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
    public class SpellingTestController : BaseTestController<ISpellingTranslationTest, TranslateQuestion>
    {
        public SpellingTestController(IMapper mapper,
            ISpellingTranslationTest spellingTest, ITestResultService testService) : base(mapper, spellingTest, testService) { }
    }
}
