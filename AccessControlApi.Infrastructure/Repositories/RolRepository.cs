using AccessControlApi.Domian.Interfaces;
using AccessControlApi.Domian.Models;
using AccessControlApi.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AccessControlApi.Infrastructure.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly ApplicationDbContext _context;

        public RolRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<Role> Create(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<bool> Delete(Role role)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetOne(int RolId)
        {
            var rol = await _context.Roles.
                Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Id == RolId);
            return rol;
        }

        public async Task<Role> GetOne(Expression<Func<Role, bool>> predicate)
        {
            var rol = await _context.Roles.FirstOrDefaultAsync(predicate);
            return rol;
        }

        public async Task<Role> Update(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return role;
        }

    }
}
