using AccessControlApi.Application.Dtos.Responses;
using AccessControlApi.Application.Eceptions;
using AccessControlApi.Domian.Interfaces;
using AccessControlApi.Domian.Models;
using AutoMapper;

namespace AccessControlApi.Application.Services
{
    public class BaseService<TEntity, TResponseDto, TCreateDto, TUpdateDto> where TEntity : BaseModel
    {
        protected readonly IBaseRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public virtual async Task<IEnumerable<TResponseDto>> GetAll()
        {
            var entities = await _repository.GetAll();
            return _mapper.Map<IEnumerable<TResponseDto>>(entities);
        }
        public virtual async Task<TResponseDto> GetOne(int id)
        {
            var entity = await _repository.GetOne(id);
            if (entity == null)
            {
                throw new NotFoundException("Record not found")
                {
                    ErrorCode = "404"
                };
            }
            return _mapper.Map<TResponseDto>(entity);
        }
        public virtual async Task<TResponseDto> Create(TCreateDto body)
        {
            var entity = _mapper.Map<TEntity>(body);
            var result = await _repository.Create(entity);
            return _mapper.Map<TResponseDto>(result);
        }
        public virtual async Task<TResponseDto> Update(int id, TUpdateDto body)
        {
            var entity = await _repository.GetOne(id);

            if (entity == null)
            {
                throw new NotFoundException("Record not found")
                {
                    ErrorCode = "404"
                };
            }

            _mapper.Map(body, entity);

            var updated = await _repository.Update(entity);
            return _mapper.Map<TResponseDto>(updated);
        }
        public virtual async Task<GenericResponseDto> Delete(int id)
        {
            var entity = await _repository.GetOne(id);

            if (entity == null)
            {
                throw new NotFoundException("Record not found")
                {
                    ErrorCode = "404"
                };
            }

            await _repository.Delete(entity);

            return new GenericResponseDto { Success = true };
        }
    }
}
