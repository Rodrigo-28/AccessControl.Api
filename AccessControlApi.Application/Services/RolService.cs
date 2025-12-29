using AccessControlApi.Application.Dtos.Requests;
using AccessControlApi.Application.Dtos.Responses;
using AccessControlApi.Application.Eceptions;
using AccessControlApi.Application.Interfaces;
using AccessControlApi.Domian.Interfaces;
using AccessControlApi.Domian.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AccessControlApi.Application.Services
{
    public class RolService : BaseService<Role, RoleResponseDto, CreateRoleDto, UpdateRoleDto>,
      IRolService
    {
        private readonly IRolRepository _rolRepository;

        public RolService(IRolRepository rolRepository, IMapper mapper)
            : base(rolRepository, mapper)
        {
            _rolRepository = rolRepository;
        }

        public override async Task<RoleResponseDto> Create(CreateRoleDto dto)
        {
            var exists = await _rolRepository.GetByName(dto.Name);
            if (exists != null)
                throw new BadRequestException("Role already exists");

            return await base.Create(dto);
        }
        public override async Task<RoleResponseDto> Update(int id, UpdateRoleDto dto)
        {
            if (dto.Name != null)
            {
                var exists = await _rolRepository.ExistsWithNameExceptId(dto.Name, id);

                if (exists != null)
                    throw new BadRequestException("Role name already exists");
            }

            return await base.Update(id, dto);
        }
        public override async Task<GenericResponseDto> Delete(int id)
        {
            var role = await _rolRepository.GetOne(
                id, q => q.Include(r => r.Users));

            if (role == null)
            {

                throw new NotFoundException("Role not found")
                {
                    ErrorCode = "011"
                };
            }

            if (role.Users.Any())
            {
                throw new BadRequestException("Cannot delete role with assigned users")
                {
                    ErrorCode = "012"
                };

            }

            return await base.Delete(id);
        }

    }
}
