using AutoMapper;
using JBC.Infrastructure.Data;
using JBC.Application.Interfaces;
using JBC.Domain.Entities;
using JBC.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JBC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractorPayController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ContractorPayController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonPayRatePerJobType>>> GetAll()
        {
            var rates = await _uow.PersonRatesPerJobType.GetAllAsync();

            var ratesDtos = _mapper.Map<IEnumerable<PersonPayRatePerJobTypeDto>>(rates);

            return Ok(ratesDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonPayRatePerJobTypeDto>> Get(int id)
        {
            var rate = await _uow.PersonRatesPerJobType.GetByIdAsync(id);

            if (rate == null) return NotFound();

            var rateDto = _mapper.Map<PersonPayRatePerJobTypeDto>(rate);

            return Ok(rateDto);
        }

        [HttpPost]
        public async Task<ActionResult<PersonPayRatePerJobTypeDto>> Create(PersonPayRatePerJobTypeDto rateDto)
        {
            var rate = _mapper.Map<PersonPayRatePerJobType>(rateDto);

            await _uow.PersonRatesPerJobType.AddAsync(rate);
            await _uow.SaveAsync();

            var responceDto = _mapper.Map<PersonPayRatePerJobTypeDto>(rate);

            return CreatedAtAction(nameof(Get), new { id = rate.Id }, responceDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PersonPayRatePerJobTypeDto rateDto)
        {
            if (id != rateDto.Id) return BadRequest();
            var rate = _mapper.Map<PersonPayRatePerJobType>(rateDto);

            _uow.PersonRatesPerJobType.Update(rate);
            await _uow.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rate = await _uow.PersonRatesPerJobType.GetByIdAsync(id);

            if (rate == null) return NotFound();

            _uow.PersonRatesPerJobType.Remove(rate);
            await _uow.SaveAsync();

            return NoContent();
        }
    }
}
