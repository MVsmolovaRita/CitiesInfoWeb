using CitiesInfoWeb;
using CitiesInfoWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController:ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<City>>GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public ActionResult<City> GetCity(int id)
        {
            
              var CityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);
                if(CityToReturn==null)
            {
                return NotFound();
            }
            return Ok(CityToReturn);    
        }
    }
}
