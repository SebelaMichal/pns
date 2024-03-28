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

        [HttpGet(Name = "GetZakaznici")]
        public IEnumerable<ZakaznikDto> GetZakaznici()
        {
            AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {
                return ZakaznikMapper.ToViewModel(db.Zakaznik).ToList();
            }
        }

        [HttpGet(Name = "GetZakaznik")]
        public IActionResult GetZakaznik(int id)
        {
            AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {
                var zakaznik = db.Zakaznik.FirstOrDefault(x => x.Id == id);
                if (zakaznik == null)
                {
                    return null;
                }
                var result = ZakaznikMapper.ToDto(zakaznik);
                return Ok(result);
                
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
        public IActionResult SmazatZakaznika(int id)
        {
            AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {
                var efZakaznik = db.Zakaznik.Find(id);
                if (efZakaznik == null)
                {
                    return NotFound();
                }

                db.Zakaznik.Remove(efZakaznik);
                db.SaveChanges();
            }

            return Ok();
        }


    }
}
