using AccessControlApi.Domian.Models;

namespace AccessControlApi.Domian.Interfaces
{
    public interface IRolRepository : IBaseRepository<Role>
    {
        Task<Role?> GetByName(string name);
        Task<bool> ExistsWithNameExceptId(string name, int excludedId);

    }
}
