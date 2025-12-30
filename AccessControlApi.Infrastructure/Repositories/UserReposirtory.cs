using AccessControlApi.Domian.Common;
using AccessControlApi.Domian.Interfaces;
using AccessControlApi.Domian.Models;
using AccessControlApi.Infrastructure.Contexts;
using AccessControlApi.Infrastructure.Utils;
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

        public async Task<GenericListResponse<User>> GetList(int page, int pageSize)
        {
            IQueryable<User> query = _context.Users
                .Include(u => u.Role)
                .Where(u => !u.Deleted)
                ;

            //Pagination
            int total = await query.CountAsync();

            //
            int currentPage = page < 1 ? PaginationConstants.DefaultPage : page;
            int currentLength = pageSize < 1 ? PaginationConstants.DefaultPageSize : pageSize;

            //
            int skip = (currentPage - 1) * currentLength;

            query = query.Skip(skip).Take(currentLength);
            var data = await query.ToListAsync();

            return new GenericListResponse<User>
            {
                Total = total,
                Page = page,
                Length = pageSize,
                Data = data
            };

        }
    }
}
