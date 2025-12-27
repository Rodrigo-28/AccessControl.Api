using AccessControlApi.Application.Dtos.Requests;
using AccessControlApi.Application.Dtos.Responses;
using AccessControlApi.Domian.Models;
using AutoMapper;

namespace AccessControlApi.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //user
            CreateMap<User, UserResponseDto>()
     .ForMember(dest => dest.Role,
                opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<CreateUserDto, User>();


            CreateMap<UpdateUserDto, User>()
     .ForMember(dest => dest.RoleId, opt => opt.Ignore())
     .ForAllMembers(opt =>
         opt.Condition((src, dest, srcMember) => srcMember != null));

            //roles
            CreateMap<Role, RoleResponseDto>();

            CreateMap<CreateRoleDto, Role>();

            CreateMap<UpdateRoleDto, Role>()
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
