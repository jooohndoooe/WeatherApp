namespace WeatherApp.Data.Storage
{
    public interface ICityRepository
    {
        IQueryable<City> GetAll();
        Task Add(City city);
        Task Remove(string locationKey);
        Task UpdateLastNotificationDate(string[] locationKeys);
    }
}
