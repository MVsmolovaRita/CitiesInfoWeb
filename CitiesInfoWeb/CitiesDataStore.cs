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
                    Description = "The most famous USA's city",
                   PointsOfInterests = new List<PointOfInterest>()
                   {
                       new PointOfInterest()
                       {
                           Id=1,
                           Name = "Times Square",
                           Description = "It's Times Square"
                       },

                       new PointOfInterest()
                       {
                           Id=2,
                           Name="Central Park",
                           Description = "It's Central Park"
                       },
                   }
                },


                new City()
                {
                    Id = 2,
                    Name = "London",
                    Description = "The capital of the Great Britan",
                    PointsOfInterests= new List<PointOfInterest>()
                    {

                        new PointOfInterest()
                        {
                            Id=1,
                            Name="London Eye",
                            Description = "It's London Eye"
                        },

                        new PointOfInterest()
                        {
                            Id=2,
                            Name="Big Ben",
                            Description = "It's Big Ben"
                        },
                    }
                },

                new City()
                {
                    Id = 3,
                    Name = "Sydney",
                    Description = "Is not the capital of Australia",
                    PointsOfInterests = new List<PointOfInterest>
                    {
                        new PointOfInterest()
                        {
                            Id=1,
                            Name="Opera House",
                            Description = "It's Opera House",
                        },

                        new PointOfInterest()
                        {
                            Id=2,
                            Name = "Tarong Zoo",
                            Description = "It's Tarong Zoo"
                        },
                    }
                }
            };
        }

    }
}
