using AutoMapper;
using BLL.Interfaces.Entities;
using EnglishApi.Dto.TestResultDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfTestingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITypeOfTestingService _typeService;
        public TypeOfTestingController(ITypeOfTestingService typeService, IMapper mapper)
        {
            _typeService = typeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<TypeOfTestingDto>>> GetAll()
        {
            var types = await _typeService.GetAllAsync();
            ICollection<TypeOfTestingDto> typesDto = _mapper.Map<ICollection<TypeOfTestingDto>>(types);
            return Ok(typesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TypeOfTestingDto>> GetById(int id)
        {
            var type = await _typeService.GetByIdAsync(id);
            TypeOfTestingDto typeDto = _mapper.Map<TypeOfTestingDto>(type);
            return Ok(typeDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Add(TypeOfTestingDto typeDto)
        {
            if (ModelState.IsValid)
            {
                var type = _mapper.Map<TypeOfTesting>(typeDto);
                await _typeService.AddAsync(type);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _typeService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(TypeOfTestingDto typeDto)
        {
            if (ModelState.IsValid)
            {
                var type = _mapper.Map<TypeOfTesting>(typeDto);
                await _typeService.UpdateAsync(type);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
