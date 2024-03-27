using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pns.Dto;
using PnsApp.Dto;
using PnsApp.Maui.Data;
using PnsApp.Maui.Mappers;

namespace PnsApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PnsController : ControllerBase
    {
      

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

        // Endpoint k editaci zakaznika
        [HttpPost(Name = "UpravitZakaznika")]
        public IActionResult UpravitZakaznika(int id, ZakaznikDto zakaznik)
        {
            AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {
                var existingZakaznik = db.Zakaznik.Find(id);
                if (existingZakaznik != null)
                {
                    db.Entry(existingZakaznik).State = EntityState.Detached;
                    var efZakaznik = ZakaznikMapper.ToEntity(zakaznik);
                    efZakaznik.Id = id;
                    db.Zakaznik.Update(efZakaznik);
                    db.SaveChanges();
                    return Ok();
                }
            
                else
                {
                    return NotFound();
                }
            }
        }



        [HttpDelete(Name = "SmazatZakaznika")]
        public IActionResult SmazatZakaznika(int id)
        {
            AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {
                var zakaznik = db.Zakaznik.Find(id);
                db.Zakaznik.Remove(zakaznik);
                db.SaveChanges();
            }

            return Ok();
        }

        [HttpGet(Name = "Naètení pozadí")]
        public IActionResult NacteniPozadi()
        {
            AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {
                var pozadi = db.Pozadi.FirstOrDefault();
                return Ok(pozadi);
            }
        }

        [HttpPost(Name = "Uložení pozadí")]
        public IActionResult UlozeniPozadi(PozadiDto pozadi)
        {
            AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {
                var efPozadi = PozadiMapper.ToEntity(pozadi);
                efPozadi.Id = 1;
                var existingPozadi = db.Pozadi.Find(efPozadi.Id);
                if (existingPozadi != null)
                {
                    db.Entry(existingPozadi).CurrentValues.SetValues(efPozadi);
                    db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
        }

    }
}
