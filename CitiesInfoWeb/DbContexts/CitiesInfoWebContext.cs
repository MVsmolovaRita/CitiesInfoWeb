using CitiesInfoWeb.Models;
using Microsoft.EntityFrameworkCore;
using CitiesInfoWeb.Entities;
namespace CitiesInfoWeb.DbContexts
{
    public class CitiesInfoWebContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointsOfInterest> PointsOfInterest { get; set; } = null!;
        public CitiesInfoWebContext(DbContextOptions<CitiesInfoWebContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                  .HasData(
                new City("New York")
                {
                    Id = 1,
                    Description = "The most famous USA's city"
                },

                new City("London")
                {
                    Id = 2,
                    Description = "The capital of the Great Britain"
                },
                new City("Sydney")
                {
                    Id = 3,
                    Description = "Is not the capital of Australia"
                });
            modelBuilder.Entity<PointsOfInterest>()
                .HasData(

                new PointsOfInterest("Times Square")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "It's Times Square"
                },
                new PointsOfInterest("Central Park")
                {
                    Id = 2,
                    CityId = 1,
                    Description = "It's Central Park"
                },
                new PointsOfInterest("London Eye")
                {
                    Id = 3,
                    CityId = 2,
                    Description = "London Eye"
                },
                new PointsOfInterest("Big Ben")
                {
                    Id = 4,
                    CityId = 2,
                    Description = "It's Big Ben"

                },
                new PointsOfInterest("Opera House")
                {
                    Id = 5,
                    CityId = 3,
                    Description = "It's Opera House"
                },
                new PointsOfInterest("Tarong Zoo")
                {
                    Id = 6,
                    CityId = 3,
                    Description = "It's Tarong Zoo"
                });
            base.OnModelCreating(modelBuilder);
              
               

        }

    }
}
