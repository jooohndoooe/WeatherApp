namespace WeatherApp.Data.Storage
{
    public class InMemoryCityStorage : ICityRepository
    {
        private List<City> Cities = new List<City>();

        public Task Add(City city)
        {
            ArgumentNullException.ThrowIfNull(city);

            Cities.Add(city);

            return Task.CompletedTask;
        }

        public IQueryable<City> GetAll()
        {
            return Cities.ToList().AsQueryable();
        }

        public Task Remove(string locationKey)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(locationKey);

            var cityToDelete = Cities.Find(x => x.LocationKey == locationKey);
            if (cityToDelete != null)
            {
                Cities.Remove(cityToDelete);
            }

            return Task.CompletedTask;
        }

        public Task UpdateLastNotificationDate(string[] locationKeys)
        {
            ArgumentNullException.ThrowIfNull(locationKeys);

            var cities = Cities.Where(e => locationKeys.Contains(e.LocationKey));
            foreach (var city in cities)
            {
                city.LastNotification = DateTime.Now;
            }

            return Task.CompletedTask;
        }
    }
}
