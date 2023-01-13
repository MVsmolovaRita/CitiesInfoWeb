using CitiesInfoWeb.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitiesInfoWeb.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Description { get; set; }

    public ICollection<PointsOfInterest> PointsOfInterest { get; set; } = new List<PointsOfInterest>();

        public City(string name)
        {
            Name = name;
        }
    }
}
