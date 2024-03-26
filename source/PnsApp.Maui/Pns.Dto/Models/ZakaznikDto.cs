using System.ComponentModel.DataAnnotations;

namespace PnsApp.Dto
{
    public class ZakaznikDto
    {
        public int Id { get; set; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
    }
}
