using AutoMapper;
using CitiesInfoWeb.Models;
using CitiesInfoWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CitiesInfoWeb.Controllers
{

    [Route("api/cities/{cityId}/pointsofinterest")]
    [Authorize]
    [ApiController]

    public class PointsOfInterestController : ControllerBase

    {
        private readonly IMailService _localMailService; //dependency injection внедрение в зависимости

        private readonly ILogger<PointsOfInterestController> _logger;

        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        private readonly ICityInfoWebRepository _cityInfoWebRepository;



        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService localMailService, IServiceProvider serviceProvider, IMapper mapper, ICityInfoWebRepository cityInfoWebRepository)
        {
            _logger = logger;
            _localMailService = localMailService;
            _serviceProvider = serviceProvider;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cityInfoWebRepository = cityInfoWebRepository ?? throw new ArgumentNullException(nameof(cityInfoWebRepository));

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDTO>>> GetPointsOfInterest(int cityId)
        {
            var cityName = User.Claims.FirstOrDefault(x => x.Type == "City")?.Value;
            if (!await _cityInfoWebRepository.CityNameMatchesCityId(cityName, cityId))
            {
                return Forbid();
            }

            if (!await _cityInfoWebRepository.CityExistAsync(cityId))
            {
                _logger.LogInformation($"{cityId} city doesn't exist");
                return NotFound();
            }
            var pointsOfInterest = await _cityInfoWebRepository.GetPointsOfInterestAsync(cityId);
            return Ok(_mapper.Map<IEnumerable<PointOfInterestDTO>>(pointsOfInterest));
        }

        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]

        public async Task<ActionResult<PointOfInterestDTO>> GetPointOfInterest(int cityId, int pointofinterestId)
        {
            

            if (!await _cityInfoWebRepository.CityExistAsync(cityId))
            {
                _logger.LogInformation($"{cityId} city doesn't exist");
                return NotFound();
            }
            var pointOfInterest = await _cityInfoWebRepository.GetPointOfInterestAsync(cityId, pointofinterestId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PointOfInterestDTO>(pointOfInterest));
        }
        [HttpPost]

        public async Task<ActionResult<PointOfInterestDTO>> CreatePointOfInterest(int cityId,
            PointOfInterestForCreation pointOfInterest)

        {

            if (!await _cityInfoWebRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }

            var createdPontOfInterest = _mapper.Map<Entities.PointsOfInterest>(pointOfInterest);
            await _cityInfoWebRepository.CreatePointOfInterestAsync(cityId, createdPontOfInterest);
            await _cityInfoWebRepository.SaveChangesAsync();
            var pointOfInterestToReturn = _mapper.Map<Models.PointOfInterestDTO>(createdPontOfInterest);
            return CreatedAtRoute("GetPointOfInterest", new
            {
                cityId = cityId,
                pointofinterestId = pointOfInterestToReturn.Id
            }, pointOfInterestToReturn);
        }

        [HttpPut("{pointofinterestid}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdate pointOfInterest)

        {

            if (!await _cityInfoWebRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }
            var pointOfInterestEntity = await _cityInfoWebRepository.GetPointOfInterestAsync(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(pointOfInterest, pointOfInterestEntity);
            await _cityInfoWebRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{pointofinterestid}")]
        public async Task<ActionResult> PartualUpdatePointOfInterest(int cityId, int pointOfInterestId,
            JsonPatchDocument<PointOfInterestForUpdate> patchDocument)
        {
            if (!await _cityInfoWebRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }
            var pointOfInterestEntity = await _cityInfoWebRepository.GetPointOfInterestAsync(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestPatch = _mapper.Map<PointOfInterestForUpdate>(pointOfInterestEntity);
            patchDocument.ApplyTo(pointOfInterestPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(pointOfInterestPatch, pointOfInterestEntity);
            await _cityInfoWebRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{pointofinterestid}")]

        public async Task<ActionResult> DeletePointOfInterest(int cityId, int pointOfInterestId)
        {

           if(!await _cityInfoWebRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }

           var pointOfInterestToDelete = await _cityInfoWebRepository.GetPointOfInterestAsync(cityId, pointOfInterestId);

            if(pointOfInterestToDelete == null)
            {
                return NotFound();
            }
            _cityInfoWebRepository.DeletePointOfInterestAsync(pointOfInterestToDelete);
            await _cityInfoWebRepository.SaveChangesAsync();
            _localMailService.Send("Point of Interest Delete ", $"{pointOfInterestToDelete.Name},with {pointOfInterestToDelete.Id} Id has been deleted");
            return NoContent();

        }

    }

}


