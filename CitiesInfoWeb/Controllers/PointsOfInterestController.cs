using CitiesInfoWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitiesInfoWeb.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
   public class PointsOfInterestController:ControllerBase
   {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterest>> GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city.PointsOfInterests);
        }

        [HttpGet("{pointofinterestId}")]

        public ActionResult<PointOfInterest> GetPointOfInterest(int cityId, int pointofinterestId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterest = city.PointsOfInterests.FirstOrDefault(x => x.Id == pointofinterestId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(pointOfInterest);
        }
    }
}
