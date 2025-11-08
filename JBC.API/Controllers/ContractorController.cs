using AutoMapper;
using JBC.Data.Interfaces;
using JBC.Domain.Entities;
using JBC.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JBC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContractorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractorDto>>> GetAll()
        {
            var contractors = await _unitOfWork.Contractors.GetAllAsync();
            var contractorDtos = _mapper.Map<IEnumerable<ContractorDto>>(contractors);

            // Load all rates once
            var allRoleRates = await _unitOfWork.RoleRatePerJobCategory
                .GetAllAsync();

            var allContractorRates = await _unitOfWork.PersonRatesPerJobType
                .GetAllAsync();

            // Populate dictionaries
            foreach (var dto in contractorDtos)
            {
                dto.RoleRates = allRoleRates
                    .Where(r => r.RoleId == dto.RoleId)
                    .ToDictionary(r => r.JobCategoryId, r => r.Pay);

                dto.ContractorRates = allContractorRates
                    .Where(c => c.ContractorId == dto.Id)
                    .ToDictionary(c => c.JobTypeId, c => c.Pay);
            }

            return Ok(contractorDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContractorDto>> GetById(int id)
        {
            var contractor = await _unitOfWork.Contractors.GetByIdAsync(id);
            if (contractor == null) return NotFound();
            var contractorDto = _mapper.Map<ContractorDto>(contractor);
            return contractorDto; 
        }

        [HttpPost]
        public async Task<ActionResult<ContractorDto>> Create(ContractorDto contractorDto)
        {
            var contractor = _mapper.Map<Contractor>(contractorDto);

            await _unitOfWork.Contractors.AddAsync(contractor);
            await _unitOfWork.SaveAsync();

            var responceDto = _mapper.Map<ContractorDto>(contractor);

            return CreatedAtAction(nameof(GetById), new { id = contractor.Id }, responceDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ContractorDto contractorDto)
        {
            if (id != contractorDto.Id) return BadRequest();

            var contractor = _mapper.Map<Contractor>(contractorDto);

            _unitOfWork.Contractors.Update(contractor);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contractor = await _unitOfWork.Contractors.GetByIdAsync(id);
            if (contractor == null) return NotFound();

            _unitOfWork.Contractors.Remove(contractor);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        //[HttpGet("statuses")]
        //public IActionResult GetStatuses()
        //{
        //    var values = Enum.GetValues(typeof(ContractorStatus))
        //        .Cast<ContractorStatus>()
        //        .Select(s => new { value = (int)s, label = s.ToString() });
        //    return Ok(values);
        //}
    }
}
