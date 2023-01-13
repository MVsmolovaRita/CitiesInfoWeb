using CitiesInfoWeb.Entities;


namespace CitiesInfoWeb.Services
{
    public interface ICityInfoWebRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync(); // возвращаем все города ассинхронно
        Task<(IEnumerable<City>, MetaDataPagination)> GetCitiesAsync(string? name, string? searchObj, int pageNumber, int pageSize);
        
        Task<City?>GetCityAsync(int cityId,bool includePointOfInterest); // возвразщаем один город ассинхронно
        Task<bool> CityExistAsync(int cityId);
        Task<bool> CityNameMatchesCityId(string? cityName, int cityId);
        Task<IEnumerable<PointsOfInterest>> GetPointsOfInterestAsync(int cityId);
        Task<PointsOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId);
        Task CreatePointOfInterestAsync(int cityId, PointsOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();
        void DeletePointOfInterestAsync(PointsOfInterest pointOfInterestToDelete);    

        
    }
}
