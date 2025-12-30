using AccessControlApi.Domian.Enums;
using AccessControlApi.Domian.Interfaces;
using AccessControlApi.Domian.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AccessControlApi.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        private readonly string _UserName;
        private readonly string _Email;
        private readonly string _Password;
        private readonly IPasswordEncryptionService _passwordEncryptionService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration, IPasswordEncryptionService passwordEncryptionService) : base(options)
        {
            _UserName = configuration["Credentials:UserName"];
            _Email = configuration["Credentials:Email"];
            _Password = configuration["Credentials:Password"];
            this._passwordEncryptionService = passwordEncryptionService;
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

            modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = _UserName, Email = _Email, Password = _passwordEncryptionService.HashPassword(_Password), FirstLogin = false, RoleId = (int)UserRole.Admin });


        }
    }
}
