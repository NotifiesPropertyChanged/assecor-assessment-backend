using Microsoft.EntityFrameworkCore;
using DotNetApiChallenge.Models;

namespace DotNetApiChallenge.DataContext
{
    public class ApiChallengeDataContext : DbContext
    {
        public DbSet<Color> Colors { get; set; }
        public DbSet<Person> Persons { get; set; }        

        public ApiChallengeDataContext(DbContextOptions<ApiChallengeDataContext> options) : base(options)
        {

        }

        //One to many are automatically inferred by EF but in case we would have any m..n relatioships,
        //here is the place to define them
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Color>(entity => {
                entity.HasIndex(e => e.Name).IsUnique();
            });

        }
    }
}
