using Pns.Dto;
using PnsApp.Dto;
using PnsApp.Maui.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnsApp.Maui.Mappers
{
    public class PozadiMapper
    {
        public static IQueryable<PozadiDto> ToViewModel(IQueryable<PozadiDto> pozadi)
        {
            return pozadi.Select(x => new PozadiDto()
            {
                BarvaPozadi = x.BarvaPozadi,
                Id = x.Id,
            });
        }

        public static Pozadi ToEntity(PozadiDto model, Pozadi entity = null)
        {
            var ent = entity ?? new Pozadi();
            ent.BarvaPozadi = (BarvaPozadi)model.BarvaPozadi;
            // ent.Id = model.Id;
            

            return ent;
        }
    }
}
