namespace CitiesInfoWeb.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<PointOfInterest> PointsOfInterests { get; set; } = new List<PointOfInterest>();
        public int NumberOfPointsOfInterest

        {
            get
            {
                return PointsOfInterests.Count;
            }
        }


    }
}
