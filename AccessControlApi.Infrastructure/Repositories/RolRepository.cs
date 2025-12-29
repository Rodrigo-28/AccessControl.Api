using AccessControlApi.Domian.Interfaces;
using AccessControlApi.Domian.Models;
using AccessControlApi.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AccessControlApi.Infrastructure.Repositories
{
    public class RolRepository : BaseRepository<Role>, IRolRepository
    {


        public RolRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<bool> ExistsWithNameExceptId(string name, int excludedId)
        {
            return await _context.Roles
        .Where(r => !r.Deleted)
        .AnyAsync(r => r.Name == name && r.Id != excludedId);
        }

        public async Task<Role?> GetByName(string name)
        {
            return await _context.Roles
                .Where(r => !r.Deleted)
                .FirstOrDefaultAsync(r => r.Name == name);
        }

    }
}
