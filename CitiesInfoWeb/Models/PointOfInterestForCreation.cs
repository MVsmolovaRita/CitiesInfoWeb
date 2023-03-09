using System.ComponentModel.DataAnnotations;

namespace CitiesInfoWeb.Models
{
    public class PointOfInterestForCreation //data transfer object DTO
    {
        [Required(ErrorMessage ="Name cannot be empty")]
        //не может быть не заполненным, только имя
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Description { get; set; }    


        

    }
}
