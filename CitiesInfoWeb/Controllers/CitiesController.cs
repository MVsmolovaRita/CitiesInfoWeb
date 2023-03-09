using AutoMapper;
using CitiesInfoWeb;
using CitiesInfoWeb.Models;
using CitiesInfoWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace CityInfo.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/cities")]
    public class CitiesController:ControllerBase
    {
        private readonly ICityInfoWebRepository _cityInfoWebRepository;
        private readonly IMapper _mapper;
        const int maxPageSize = 20;
        public CitiesController(ICityInfoWebRepository cityInfoWebRepository, IMapper mapper)
        {
            _cityInfoWebRepository = cityInfoWebRepository;
            _mapper = mapper;
        }
 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterest>>> 
            GetCities([FromQuery] string? name, string? searchObj, int pageNumber=1, int pageSize=10)
        { 
            if(pageSize>maxPageSize)
            {
                pageSize = maxPageSize;
            }
            var (cities, metaDataPagination) =
                await _cityInfoWebRepository.GetCitiesAsync(name, searchObj, pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metaDataPagination));
        return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterest>>(cities));

        }
        


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id, bool includePointOfInterest=false)
        {
            var city = await _cityInfoWebRepository.GetCityAsync(id, includePointOfInterest);
            if(city == null)
            {
                return NotFound();  
            }
            if(includePointOfInterest==true)
            {
                return Ok(_mapper.Map<CityDTO>(city));
            }
            return Ok(_mapper.Map<CityWithoutPointsOfInterest>(city));
        }
    }
}
//branch4