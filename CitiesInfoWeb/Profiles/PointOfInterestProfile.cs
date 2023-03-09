using AutoMapper;
using System.Security.Cryptography.X509Certificates;

namespace CitiesInfoWeb.Profiles
{
    public class PointOfInterestProfile:Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap <Entities.PointsOfInterest, Models.PointOfInterestDTO>();
            CreateMap<Models.PointOfInterestForCreation, Entities.PointsOfInterest>();
            CreateMap<Models.PointOfInterestForUpdate, Entities.PointsOfInterest>();
            CreateMap<Entities.PointsOfInterest, Models.PointOfInterestForUpdate>();

        }


    }
}
