using PnsApp.Dto;
using PnsApp.Maui.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnsApp.Maui.Mappers
{
    public class ZakaznikMapper
    {
        public static IQueryable<ZakaznikDto> ToViewModel(IQueryable<Zakaznik> zakaznici)
        {
            return zakaznici.Select(x => new ZakaznikDto()
            {
                Jmeno = x.Jmeno,
                Prijmeni = x.Prijmeni,
                Telefon = x.Telefon,
                Email = x.Email,
                Id = x.Id,
            });
        }

        public static Zakaznik ToEntity(ZakaznikDto model, Zakaznik entity = null)
        {
            var zak = entity ?? new Zakaznik();
            zak.Jmeno = model.Jmeno;
            zak.Prijmeni = model.Prijmeni;
            zak.Telefon = model.Telefon;
            zak.Email = model.Email;
            //zak.Id = model.Id;

            return zak;
        }

        
    }
}
