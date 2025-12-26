using AccessControlApi.Domian.Enums;
using AccessControlApi.Domian.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessControlApi.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity
                    .HasOne(u => u.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(u => u.RoleId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Role>().HasData(
      new Role { Id = (int)UserRole.Admin, Name = "Admin" },
      new Role { Id = (int)UserRole.User, Name = "User" }


        );
        }
    }
}
