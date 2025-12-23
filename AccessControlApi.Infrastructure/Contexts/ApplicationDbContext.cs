using AccessControlApi.Domian.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessControlApi.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Role { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity
                    .HasOne(u => u.Role)          // User tiene UN Role
                    .WithMany(r => r.Users)       // Role tiene MUCHOS Users
                    .HasForeignKey(u => u.RoleId) // FK en User
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);// Obligatoria
            });
        }
    }
}
