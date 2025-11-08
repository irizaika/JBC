using AutoMapper;
using JBC.Data;
using JBC.Data.Interfaces;
using JBC.Domain.Entities;
using JBC.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JBC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartnerController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PartnerController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartnerDto>>> GetAll()
        {
            var partners = await _uow.Partners.GetAllAsync();
            var partnerDtos = _mapper.Map<IEnumerable<PartnerDto>>(partners);
            return Ok(partnerDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PartnerDto>> Get(int id)
        {
            var partner = await _uow.Partners.GetByIdAsync(id);
            if (partner == null)  return NotFound();

            var partnerDto = _mapper.Map<PartnerDto>(partner);
                    
            return Ok(partnerDto);
        }

        [HttpPost]
        public async Task<ActionResult<PartnerDto>> Create(PartnerDto partnerDto)
        {
            var partner = _mapper.Map<Partner>(partnerDto);

            await _uow.Partners.AddAsync(partner);
            await _uow.SaveAsync();

            var responceDto = _mapper.Map<PartnerDto>(partner);


            return CreatedAtAction(nameof(Get), new { id = partnerDto.Id }, responceDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PartnerDto partnerDto)
        {
            if (id != partnerDto.Id) return BadRequest();
            var partner = _mapper.Map<Partner>(partnerDto);

            _uow.Partners.Update(partner);
            await _uow.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var partner = await _uow.Partners.GetByIdAsync(id);
            if (partner == null) return NotFound();
            _uow.Partners.Remove(partner);
            await _uow.SaveAsync();
            return NoContent();
        }
    }
}
