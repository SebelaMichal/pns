using Microsoft.AspNetCore.Mvc;
using PnsApp.Dto;
using PnsApp.Maui.Data;
using PnsApp.Maui.Mappers;
using PnsApp.WebApi.PomocneTridy;

namespace PnsApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PnsController : ControllerBase
    {
        private SingletonTrida _singletonTrida;
        private ScopeTrida _scope;
        private TransientTrida _transient;
        private AppDbContext _db;

        public PnsController(SingletonTrida singleton, ScopeTrida scope, TransientTrida transient, AppDbContext db)
        {
            _singletonTrida = singleton;
            _scope = scope;
            _transient = transient;
            _db = db;
        }

        [HttpGet(Name = "GetZakaznici")]
        public IEnumerable<ZakaznikDto> GetZakaznici()
        {
            /*
            AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {*/
                return ZakaznikMapper.ToViewModel(_db.Zakaznik).ToList();
            //}
        }

        [HttpGet(Name = "GetZakaznik")]
        public IActionResult GetZakaznik(int id)
        {
            /*AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {*/
                var zakaznik = _db.Zakaznik.FirstOrDefault(x => x.Id == id);
                if (zakaznik == null)
                {
                    return NotFound();
                }
                var result = ZakaznikMapper.ToDto(zakaznik);
                return Ok(result);
                
            //}
        }

        [HttpPost(Name = "PridatZakaznika")]
        public IActionResult PridatZakaznika(ZakaznikDto zakaznik)
        {
            /*
            AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {*/
                var efZakaznik = ZakaznikMapper.ToEntity(zakaznik);
                _db.Zakaznik.Add(efZakaznik);
                _db.SaveChanges();
           // }

            return Ok();
        }

        [HttpPut(Name = "UpravitZakaznika")]
        public IActionResult UpravitZakaznika(ZakaznikDto zakaznik)
        {
            /*
            AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {*/
                var efZakaznik = _db.Zakaznik.Find(zakaznik.Id);
                if (efZakaznik == null)
                {
                    return NotFound();
                }

                ZakaznikMapper.ToEntity(zakaznik, efZakaznik);
                _db.SaveChanges();
            //}

            return Ok();
        }

        [HttpDelete(Name = "SmazatZakaznika")]
        public IActionResult SmazatZakaznika([FromQuery] int id)
        {
            /*
            AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {*/
                var efZakaznik = _db.Zakaznik.Find(id);
                if (efZakaznik == null)
                {
                    return NotFound();
                }

                _db.Zakaznik.Remove(efZakaznik);
                _db.SaveChanges();
            //}

            return Ok();
        }
    }
}
