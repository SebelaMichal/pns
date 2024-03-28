using Pns.Dto;
using Pns.Dto.Models;
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
        public static IQueryable<BarvaPozadiDto> ToViewModel(IQueryable<BarvaPozadiDto> pozadi)
        {
            return pozadi.Select(x => new BarvaPozadiDto()
            {
                BarvaPozadi = x.BarvaPozadi,
                Id = x.Id,
            });
        }

        public static Pozadi ToEntity(BarvaPozadiDto model, Pozadi entity = null)
        {
            var ent = entity ?? new Pozadi();
            ent.BarvaPozadi = (BarvaPozadiDto)model.BarvaPozadi;
            // ent.Id = model.Id;


            return ent;
        }
    }
}
