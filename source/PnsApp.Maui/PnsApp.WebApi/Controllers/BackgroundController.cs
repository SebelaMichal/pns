using Microsoft.AspNetCore.Mvc;
using PnsApp.Dto;
using PnsApp.Maui.Data;
using PnsApp.Maui.Mappers;

namespace PnsApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BackgroundController : ControllerBase
    {
        private AppDbContext _db;
        public BackgroundController(AppDbContext db) 
        {
            _db = db;
        }

        [HttpGet(Name = "NacistBarvu")]
        public IActionResult NacistBarvu()
        {
            /*
            AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {*/
                var data = _db.Pozadi.FirstOrDefault();
                if (data == null)
                {
                    return null;
                }
                var result = BarvaMapper.ToDto(data);
                return Ok(result.BarvaPozadi);
            //}
        }


        [HttpPut(Name = "UpravitBarvu")]
        public IActionResult UpravitBarvu(int id)
        {
            /*
            AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {*/
                var efData = _db.Pozadi.Where(x => x.Id == 1).FirstOrDefault();
                if (efData == null)
                {
                    return NotFound();
                }

                BarvaMapper.ToEntity(id, efData);
                _db.SaveChanges();
            //}

            return Ok();
        }

    }
}
