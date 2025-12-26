using AccessControlApi.Domian.Models;
using System.Linq.Expressions;

namespace AccessControlApi.Domian.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetOne(int userId);
        Task<User> Create(User user);
        Task<User> Update(User user);
        Task<User?> GetOne(Expression<Func<User, bool>> predicate);
        Task<bool> Delete(User user);
        Task<User?> GetOneWithRole(int userId);
        Task<IEnumerable<User>> GetAllWithRoles();
    }
}
