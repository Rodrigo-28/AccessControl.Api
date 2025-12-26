using AccessControlApi.Domian.Models;
using System.Linq.Expressions;

namespace AccessControlApi.Domian.Interfaces
{
    public interface IRolRepository
    {
        Task<Role> Create(Role role);
        Task<Role> GetOne(int RolId);
        Task<Role> GetOne(Expression<Func<Role, bool>> predicate);
        Task<IEnumerable<Role>> GetAll();
        Task<Role> Update(Role role);
    }
}
