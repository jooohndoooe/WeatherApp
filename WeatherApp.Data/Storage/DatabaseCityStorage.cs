using Microsoft.EntityFrameworkCore;

namespace WeatherApp.Data.Storage
{
    public class DatabaseCityStorage : ICityRepository
    {
        private readonly WeatherDbContext db;
        public DatabaseCityStorage(WeatherDbContext db)
        {
            ArgumentNullException.ThrowIfNull(db);
            this.db = db;
        }
        public async Task Add(City city)
        {
            ArgumentNullException.ThrowIfNull(city);

            var exitignCity = await db.Cities.FirstOrDefaultAsync(e => e.LocationKey == city.LocationKey);

            if (exitignCity != null)
            {
                throw new InvalidOperationException("City already Added");
            }
            await db.Cities.AddAsync(city);
            await db.SaveChangesAsync();
        }

        public IQueryable<City> GetAll()
        {
            return db.Cities;
        }

        public async Task Remove(string locationKey)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(locationKey);

            var cityToDelete = await db.Cities.FindAsync(locationKey);
            if (cityToDelete != null)
            {
                db.Cities.Remove(cityToDelete);
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateLastNotificationDate(string[] locationKeys)
        {
            ArgumentNullException.ThrowIfNull(locationKeys);

            var cities = await db.Cities.Where(e => locationKeys.Contains(e.LocationKey)).ToArrayAsync();
            foreach (var city in cities) 
            { 
                city.LastNotification = DateTime.Now;
            }

            await db.SaveChangesAsync();
        }
    }
}
