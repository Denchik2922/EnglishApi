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
    public class AudioTestController : BaseTestController<IAudioTranslateTest, AudioQuestion>
    {
        public AudioTestController(IMapper mapper,
            IAudioTranslateTest audioTest, ITestResultService testService): base(mapper, audioTest, testService){ }
    }
}
