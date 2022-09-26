using Microsoft.EntityFrameworkCore;
using Myra.Domain.Core;
using Myra.Domain.Models;
using Myra.Domain.Models.Enumerations;
using Myra.Infra.Utils;

namespace Myra.Infra.Context
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.Entity<UserRole>()
                .HasData(Enumeration.GetAll<UserRole>());
            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    Name = "Root Admin",
                    Password = PasswordHasher.Hash("admin"),
                    UserName = "admin",
                    Email = "admin@example.com",
                    IdUserRole = UserRole.Admin.Id
                });
        }
    }
}
