using CitiesInfoWeb.Models;

namespace CitiesInfoWeb
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; }= new CitiesDataStore();
        public List<City> Cities { get; set; }
        public CitiesDataStore()
        {
            Cities = new List<City>()
            {
                new City()
                {
                    Id = 1,
                    Name = "New York",
                    Description = "The most famous USA's city"
                },


                new City()
                {
                    Id = 2,
                    Name = "London",
                    Description = "The capital of the Great Britan"
                },

                new City()
                {
                    Id = 3,
                    Name = "Sydney",
                    Description = "Is not the capital of Australia"
                }
            };
        }

    }
}
