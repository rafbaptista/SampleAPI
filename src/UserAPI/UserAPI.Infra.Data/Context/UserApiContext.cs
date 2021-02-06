using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UserAPI.Domain.Entities;
using UserAPI.Domain.Enums;
using UserAPI.Infra.Data.Extensions;

namespace UserAPI.Infra.Data.Context
{
    public class UserApiContext : DbContext
    {
        public UserApiContext(DbContextOptions options)
            :base(options)
        {
            //create in memory database & seed
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserApiContext).Assembly);
            modelBuilder.SeedInitialData();
        } 
    }
}
