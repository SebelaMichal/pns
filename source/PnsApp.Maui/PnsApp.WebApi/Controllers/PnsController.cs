using Microsoft.AspNetCore.Mvc;
using Pns.Dto.Models;
using PnsApp.Dto;
using PnsApp.Maui.Data;
using PnsApp.Maui.Mappers;

namespace PnsApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PnsController : ControllerBase
    {

        [HttpGet(Name = "GetZakaznici")]
        public IEnumerable<ZakaznikDto> GetZakaznici()
        {
            AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {
                return ZakaznikMapper.ToViewModel(db.Zakaznik).ToList();
            }
        }

        [HttpPost(Name = "PridatZakaznika")]
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

        [HttpPut(Name = "UpravitZakaznika")]
        public IActionResult UpravitZakaznika(ZakaznikDto zakaznik)
        {
            AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {
                var efZakaznik = db.Zakaznik.Find(zakaznik.Id);
                if (efZakaznik == null)
                {
                    return NotFound();
                }

                ZakaznikMapper.ToEntity(zakaznik, efZakaznik);
                db.SaveChanges();
            }

            return Ok();
        }

        [HttpDelete(Name = "SmazatZakaznika")]
        public IActionResult SmazatZakaznika([FromQuery] int id)
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
        public IActionResult UlozeniPozadi(BarvaPozadiDto pozadi)
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
