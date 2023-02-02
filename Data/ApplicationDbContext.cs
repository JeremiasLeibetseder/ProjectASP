using ProjectASP.Data.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using ProjectASP.Data.Entities;



namespace ProjectASP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Train> Train { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Train>().HasData(
                new Train()
                {
                    Id = 1,
                    Name = "10OFF",
                    Length = 10,
                    IsActive = true
                },
                new Train()
                {
                    Id = 2,
                    Name = "20OFF",
                    Length = 20,
                    IsActive = true
                });
        }
    }
}