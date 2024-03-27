using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pns.Dto
{
    public class PozadiDto
    {
        public enum BarvaPozadiDto : int
        {
            Cervena = 1,
            Zelena = 2,
            Modra = 3,
            Bila = 4
        }

        public int Id { get; set; }

        public BarvaPozadiDto BarvaPozadi { get; set; }

    }
}
