using AccessControlApi.Domian.Interfaces;
using AccessControlApi.Domian.Models;
using AccessControlApi.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AccessControlApi.Infrastructure.Repositories
{
    public class UserReposirtory : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserReposirtory(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<User> Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> Delete(User user)
        {
            user.Deleted = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetAll()
        {

            return await _context.Users.Where(u => !u.Deleted).ToListAsync();
        }

        public async Task<User?> GetOne(int userId)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId && !u.Deleted);
        }



        public async Task<User> Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User?> GetOneWithRole(int userId)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<IEnumerable<User>> GetAllWithRoles()
        {
            return await _context.Users
                .Where(u => !u.Deleted)
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task<User?> GetOneByEmail(string email)
        {
            return await _context.Users.Where(u => u.Email == email && !u.Deleted).FirstOrDefaultAsync();
        }


    }
}
