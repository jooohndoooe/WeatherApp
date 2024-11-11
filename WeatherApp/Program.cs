using System.Text.Json;
using WeatherApp.Core.CityWeatherService;
using WeatherApp.Data;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    WebRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "ui")
});
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true)
                                                  .AddJsonFile(
                                                       $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                                                       true
                                                   )
                                                  .AddEnvironmentVariables()
                                                  .Build();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
builder.Services.AddCityWeatherService();
builder.Services.AddDataConfiguration(configuration.GetConnectionString("DefaultConnection"), configuration.GetValue<string>("AccuWeatherApiKey"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.UseDefaultFiles();
app.UseStaticFiles();
app.Run();
