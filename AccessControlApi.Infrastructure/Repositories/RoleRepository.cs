using AccessControlApi.Domian.Interfaces;
using AccessControlApi.Domian.Models;
using AccessControlApi.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AccessControlApi.Infrastructure.Repositories
{
    public class RoleRepository : IRolRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
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
            role.Deleted = true;
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.Roles
                .Where(r => !r.Deleted)
                .ToListAsync();
        }

        public async Task<Role> GetOne(int RolId)
        {
            var role = await _context.Roles
                .Where(r => !r.Deleted)
                .FirstOrDefaultAsync(r => r.Id == RolId);
            return role;
        }

        public async Task<Role> GetOne(Expression<Func<Role, bool>> predicate)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(predicate);
            return role;
        }

        public async Task<Role> Update(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return role;
        }

    }
}
