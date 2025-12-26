using AccessControlApi.Application.Dtos.Requests;
using AccessControlApi.Application.Dtos.Responses;
using AccessControlApi.Domian.Models;
using System.Linq.Expressions;

namespace AccessControlApi.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAll();
        Task<UserResponseDto> GetOne(int userId);
        Task<UserResponseDto> Create(CreateUserDto createUserDto);

        Task<UserResponseDto> Update(int userId, UpdateUserDto updateUserDto);
        Task<GenericResponseDto> Delete(int userId);
        Task<User> GetOne(Expression<Func<User, bool>> predicate);
    }
}
