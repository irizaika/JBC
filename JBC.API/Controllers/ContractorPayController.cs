using JBC.Application.Intefraces.CrudInterfaces;
using JBC.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JBC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractorPayController : ControllerBase
    {
        private readonly IContractorPayService _contractorPayService;

        public ContractorPayController(IContractorPayService contractorPayService)
        {
            _contractorPayService = contractorPayService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonPayRatePerJobTypeDto>>> GetAll()
        {
            var contractorPayDtos = await _contractorPayService.GetAllAsync();
            return Ok(contractorPayDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonPayRatePerJobTypeDto>> Get(int id)
        {
            var contractorPayDto = await _contractorPayService.GetByIdAsync(id);
            if (contractorPayDto == null) return NotFound();
            return Ok(contractorPayDto);
        }

        [HttpPost]
        public async Task<ActionResult<PersonPayRatePerJobTypeDto>> Create(PersonPayRatePerJobTypeDto contractorPayDto)
        {
            var result = await _contractorPayService.CreateAsync(contractorPayDto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PersonPayRatePerJobTypeDto contractorPayDto)
        {
            await _contractorPayService.UpdateAsync(id, contractorPayDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _contractorPayService.DeleteAsync(id);
            return NoContent();
        }
    }
}
