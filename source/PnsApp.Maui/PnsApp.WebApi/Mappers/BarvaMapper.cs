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
    public class BarvaMapper
    {
        public static BarvaDto ToDto(Pozadi pozadi)
        {
            return new BarvaDto
            {
                Id = pozadi.Id,
                BarvaPozadi = (int)pozadi.BarvaPozadi

            };
        }

        public static Pozadi ToEntity(int id, Pozadi pozadi = null)
        {
            var bg = pozadi ?? new Pozadi();
            bg.BarvaPozadi = (BarvaPozadi)id;

            return bg;
        }

      
       

        








    }
}
