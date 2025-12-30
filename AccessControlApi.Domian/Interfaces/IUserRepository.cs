using AccessControlApi.Domian.Common;
using AccessControlApi.Domian.Models;

namespace AccessControlApi.Domian.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetOne(int userId);
        Task<User> Create(User user);
        Task<User> Update(User user);
        Task<User?> GetOneByEmail(string email);
        Task<bool> Delete(User user);
        Task<User?> GetOneWithRole(int userId);
        Task<IEnumerable<User>> GetAllWithRoles();
        Task<GenericListResponse<User>> GetList(int page, int pageSize);

    }
}
