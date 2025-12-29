using AccessControlApi.Domian.Models;

namespace AccessControlApi.Domian.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetOneWithRole(int userId);

        Task<IEnumerable<User>> GetAllWithRoles();

        Task<User?> GetOneByEmail(string email);


    }
}
