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

        [HttpGet(Name = "NacistBarvu")]
        public IActionResult NacistBarvu()
        {
            AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {
                var data = db.Pozadi.FirstOrDefault();
                if (data == null)
                {
                    return null;
                }
                var result = BarvaMapper.ToDto(data);
                return Ok(result.BarvaPozadi);
            }
        }


        [HttpPut(Name = "UpravitBarvu")]
        public IActionResult UpravitBarvu(int id)
        {
            AppDbContextFactory factory = new AppDbContextFactory();
            using (var db = factory.CreateDbContext(null))
            {
                var efData = db.Pozadi.Where(x => x.Id == 1).FirstOrDefault();
                if (efData == null)
                {
                    return NotFound();
                }

                BarvaMapper.ToEntity(id, efData);
                db.SaveChanges();
            }

            return Ok();
        }

    }
}
