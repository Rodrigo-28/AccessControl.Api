using AccessControlApi.Domian.Interfaces;
using AccessControlApi.Domian.Models;
using AccessControlApi.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AccessControlApi.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {


        public UserRepository(ApplicationDbContext context)
        : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetAllWithRoles()
        {
            return await _dbSet
                .Where(u => !u.Deleted)
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task<User?> GetOneByEmail(string email)
        {
            return await _dbSet
               .FirstOrDefaultAsync(u => u.Email == email && !u.Deleted);
        }

        public async Task<User?> GetOneWithRole(int userId)
        {
            return await _dbSet
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId && !u.Deleted);
        }

    }
}
