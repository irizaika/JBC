using JBC.Application.Intefraces.CrudInterfaces;
using JBC.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JBC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractorController : ControllerBase
    {
        private readonly IContractorService _service;

        public ContractorController(IContractorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractorDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContractorDto>> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ContractorDto>> Create(ContractorDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ContractorDto dto)
        {
            if (id != dto.Id) return BadRequest();
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
