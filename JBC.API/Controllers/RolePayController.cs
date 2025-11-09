using AutoMapper;
using JBC.Data;
using JBC.Application.Interfaces;
using JBC.Domain.Entities;
using JBC.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JBC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolePayController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public RolePayController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolePayRatePerJobCategoryDto>>> GetAll()
        {
            var rates = await _uow.RoleRatePerJobCategory.GetAllAsync();

            var ratesDtos = _mapper.Map<IEnumerable<RolePayRatePerJobCategoryDto>>(rates);

            return Ok(ratesDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RolePayRatePerJobCategoryDto>> Get(int id)
        {
            var rate = await _uow.RoleRatePerJobCategory.GetByIdAsync(id);

            if (rate == null) return NotFound();

            var rateDto = _mapper.Map<RolePayRatePerJobCategoryDto>(rate);

            return Ok(rateDto);
        }

        [HttpPost]
        public async Task<ActionResult<RolePayRatePerJobCategoryDto>> Create(RolePayRatePerJobCategoryDto rateDto)
        {
            var rate = _mapper.Map<RolePayRatePerJobCategory>(rateDto);

            await _uow.RoleRatePerJobCategory.AddAsync(rate);
            await _uow.SaveAsync();

            var responceDto = _mapper.Map<RolePayRatePerJobCategoryDto>(rate);

            return CreatedAtAction(nameof(Get), new { id = rate.Id }, responceDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RolePayRatePerJobCategoryDto rateDto)
        {
            if (id != rateDto.Id) return BadRequest();
            var rate = _mapper.Map<RolePayRatePerJobCategory>(rateDto);

            _uow.RoleRatePerJobCategory.Update(rate);
            await _uow.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rate = await _uow.RoleRatePerJobCategory.GetByIdAsync(id);

            if (rate == null) return NotFound();

            _uow.RoleRatePerJobCategory.Remove(rate);
            await _uow.SaveAsync();

            return NoContent();
        }
    }
}
