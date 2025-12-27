using AccessControlApi.Application.Dtos.Requests;
using AccessControlApi.Application.Dtos.Responses;
using AccessControlApi.Application.Eceptions;
using AccessControlApi.Application.Interfaces;
using AccessControlApi.Domian.Interfaces;
using AccessControlApi.Domian.Models;
using AutoMapper;

namespace AccessControlApi.Application.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;
        private readonly IMapper _mapper;

        public RolService(IRolRepository rolRepository, IMapper mapper)
        {
            this._rolRepository = rolRepository;
            this._mapper = mapper;
        }
        public async Task<RoleResponseDto?> Create(CreateRoleDto createRoleDto)
        {
            var existingRole = await _rolRepository.GetOne(r => r.Name == createRoleDto.Name);
            if (existingRole != null)
            {
                throw new BadRequestException("Role already exists")
                {
                    ErrorCode = "007"
                };
            }
            var role = _mapper.Map<Role>(createRoleDto);
            var createRole = await _rolRepository.Create(role);
            return _mapper.Map<RoleResponseDto>(createRole);
        }

        public async Task<GenericResponseDto> Delete(int roleId)
        {
            var rol = await _rolRepository.GetOne(roleId);
            if (rol == null)
            {
                throw new NotFoundException($"rol of id {roleId} does not exist ")
                {
                    ErrorCode = "009"
                };
            }
            if (rol.Users != null && rol.Users.Any())
            {
                throw new BadRequestException("Cannot delete role with assigned users");
            }

            await _rolRepository.Delete(rol);
            return new GenericResponseDto { Success = true };
        }

        public async Task<IEnumerable<RoleResponseDto>> GetAll()
        {
            var role = await _rolRepository.GetAll();
            return _mapper.Map<IEnumerable<RoleResponseDto>>(role);
        }

        public async Task<RoleResponseDto?> GetOne(int rolId)
        {
            var role = await _rolRepository.GetOne(rolId);
            if (role == null)
            {
                throw new NotFoundException($"Role with id {rolId} not found")
                {
                    ErrorCode = "008"
                };
            }
            return _mapper.Map<RoleResponseDto>(role);

        }

        public async Task<RoleResponseDto> Update(int roleId, UpdateRoleDto updateRoleDto)
        {
            var role = await _rolRepository.GetOne(roleId);

            if (role == null)
            {
                throw new NotFoundException($"Role with id {roleId} not found")
                {
                    ErrorCode = "007"
                };
            }
            if (updateRoleDto.Name != null)
            {
                var exists = await _rolRepository.GetOne(
                    r => r.Name == updateRoleDto.Name && r.Id != roleId);

                if (exists != null)
                {
                    throw new BadRequestException("Role name already exists");
                }
            }
            _mapper.Map(updateRoleDto, role);

            var updatedRole = await _rolRepository.Update(role);

            return _mapper.Map<RoleResponseDto>(updatedRole);

        }


    }
}
