using AutoMapper;
using JBC.Application.Interfaces;

namespace JBC.Application.Services
{
    public abstract class CrudService<TDto, TEntity> : ICrudService<TDto>
        where TEntity : class
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IMapper _mapper;
        protected readonly IGenericRepository<TEntity> _repo;

        protected CrudService(IUnitOfWork uow, IMapper mapper, IGenericRepository<TEntity> repo)
        {
            _uow = uow;
            _mapper = mapper;
            _repo = repo;
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<TDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repo.AddAsync(entity);
            await _uow.SaveAsync();
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task UpdateAsync(int id, TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _repo.Update(entity);
            await _uow.SaveAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity != null)
            {
                _repo.Remove(entity);
                await _uow.SaveAsync();
            }
        }
    }
}
