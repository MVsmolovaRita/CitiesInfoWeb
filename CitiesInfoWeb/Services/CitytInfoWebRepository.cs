using CitiesInfoWeb.DbContexts;
using CitiesInfoWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace CitiesInfoWeb.Services
{
    public class CitytInfoWebRepository : ICityInfoWebRepository
    {
        private readonly CitiesInfoWebContext _citiesInfoWebContext;
        public CitytInfoWebRepository(CitiesInfoWebContext citiesInfoWebContext)
        {
            _citiesInfoWebContext = citiesInfoWebContext;
        }
        #region City
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _citiesInfoWebContext.Cities.OrderBy(x => x.Name).ToListAsync();

        }
        public async Task<(IEnumerable<City>, MetaDataPagination)>
            GetCitiesAsync(string? name, string? searchObj, int pageNumber, int pageSize)
        {

            //if (string.IsNullOrEmpty(name)&& string.IsNullOrEmpty(searchObj))
            //{
            //    return await GetCitiesAsync();  
            //}

            var collection = _citiesInfoWebContext.Cities as IQueryable<City>;
            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                collection = collection.Where(x => x.Name == name);
            }

            if (!string.IsNullOrEmpty(searchObj))
            {
                searchObj = searchObj.Trim();
                collection = collection.Where(x => x.Name.Contains(searchObj) || (x.Description != null && x.Description.Contains(searchObj)));
            }
            var totalItems = await collection.CountAsync();
            var metaDataPagination = new MetaDataPagination(pageSize, pageNumber, totalItems);
            var collectionCitiesToReturn = await collection.OrderBy(x => x.Id)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return (collectionCitiesToReturn, metaDataPagination);  
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointOfInterest)
        {
            if (includePointOfInterest)
            {
                return await _citiesInfoWebContext.Cities.Include(x => x.PointsOfInterest).Where
                    (x => x.Id == cityId).FirstOrDefaultAsync();
            }
            return await _citiesInfoWebContext.Cities.Where(x => x.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<bool> CityExistAsync(int cityId)
        {
            return await _citiesInfoWebContext.Cities.AnyAsync(x => x.Id == cityId);
        }
        public async Task<bool> CityNameMatchesCityId(string? cityName, int cityId)
        {
            return await _citiesInfoWebContext.Cities.AnyAsync(x => x.Id == cityId && x.Name == cityName);

        }
        #endregion

        #region PointOfInterest
        public async Task<PointsOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId)
        {
            return await _citiesInfoWebContext.PointsOfInterest.Where(x => x.CityId == cityId && x.Id == pointOfInterestId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointsOfInterest>> GetPointsOfInterestAsync(int cityId)
        {
            return await _citiesInfoWebContext.PointsOfInterest.Where(x => x.CityId == cityId).ToListAsync();
        }

        public async Task CreatePointOfInterestAsync(int cityId, PointsOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);
            if (city != null)
            {
                city.PointsOfInterest.Add(pointOfInterest);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _citiesInfoWebContext.SaveChangesAsync() > 0);
        }

        public void DeletePointOfInterestAsync(PointsOfInterest pointOfInterestToDelete)
        {
            _citiesInfoWebContext.PointsOfInterest.Remove(pointOfInterestToDelete);

        }



        #endregion


    }

}
