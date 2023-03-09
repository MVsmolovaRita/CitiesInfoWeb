using CitiesInfoWeb.Entities;

namespace CitiesInfoWeb.Models
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<PointOfInterestDTO> PointsOfInterests { get; set; } = new List<PointOfInterestDTO>();
        public int NumberOfPointsOfInterest

        {
            get
            {
                return PointsOfInterests.Count;
            }
        }


    }
}
