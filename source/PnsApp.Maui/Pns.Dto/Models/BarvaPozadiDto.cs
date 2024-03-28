using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pns.Dto.Models
{
    public enum BarvaPozadi : int
    {
        Cervena = 1,
        Zelena = 2,
        Modra = 3
    }


    public class BarvaPozadiDto
    {
        public int Id { get; set; }
        public BarvaPozadiDto BarvaPozadi { get; set; }
    }
}
