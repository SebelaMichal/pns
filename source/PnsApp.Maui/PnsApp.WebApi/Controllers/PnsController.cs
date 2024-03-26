using Microsoft.AspNetCore.Mvc;
using PnsApp.Dto;
using PnsApp.Maui.Data;
using PnsApp.Maui.Mappers;

namespace PnsApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PnsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public PnsController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        /*
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }*/

        [HttpGet(Name = "GetZakaznici")]
        public IEnumerable<ZakaznikDto> GetZakaznici()
        {
            AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {
                //detailZakaznikaListView.ItemsSource = ZakaznikMapper.ToViewModel(db.Zakaznik).ToList();

                return ZakaznikMapper.ToViewModel(db.Zakaznik).ToList();
            }
        }

        [HttpPut(Name = "PridatZakaznika")]
        public IActionResult PridatZakaznika(ZakaznikDto zakaznik)
        {
            AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {
                var efZakaznik = ZakaznikMapper.ToEntity(zakaznik);
                db.Zakaznik.Add(efZakaznik);
                db.SaveChanges();
            }

            return Ok();
        }


    }
}
