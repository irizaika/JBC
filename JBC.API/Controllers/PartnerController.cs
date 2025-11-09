using JBC.Application.Intefraces.CrudInterfaces;
using JBC.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JBC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartnerController : ControllerBase
    {
        private readonly IPartnerService _partnerService;

        public PartnerController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartnerDto>>> GetAll()
        {
            var partners = await _partnerService.GetAllAsync();
            return Ok(partners);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PartnerDto>> Get(int id)
        {
            var partner = await _partnerService.GetByIdAsync(id);
            if (partner == null) return NotFound();
            return Ok(partner);
        }

        [HttpPost]
        public async Task<ActionResult<PartnerDto>> Create(PartnerDto partnerDto)
        {
            var result = await _partnerService.CreateAsync(partnerDto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PartnerDto partnerDto)
        {
            await _partnerService.UpdateAsync(id, partnerDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _partnerService.DeleteAsync(id);
            return NoContent();
        }
    }
}
