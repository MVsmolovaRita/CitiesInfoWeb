using AutoMapper;

namespace CitiesInfoWeb.Profiles
{
    public class CityProfile:Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.City, Models.CityWithoutPointsOfInterest>();
            CreateMap<Entities.City, Models.CityDTO>();
        }

    }
}
