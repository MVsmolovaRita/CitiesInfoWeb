using CitiesInfoWeb;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController:ControllerBase
    {
        [HttpGet]
        public JsonResult GetCities()

        {

            return new JsonResult(

                CitiesDataStore.Current.Cities
                ) ;
        }


    }
}
