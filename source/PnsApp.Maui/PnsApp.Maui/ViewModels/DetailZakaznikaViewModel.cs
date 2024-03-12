using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnsApp.Maui.ViewModels
{
    public class DetailZakaznikaViewModel: BindableObject
    {
        private int _id;

        public int Id { get { return _id; } set { _id = value; OnPropertyChanged(nameof(Id)); } }

        private string _jmeno;
        public string Jmeno { get { return _jmeno; } set { _jmeno = value; OnPropertyChanged(nameof(Jmeno)); } }

        private string _prijmeni;
        public string Prijmeni { get { return _prijmeni; } set { _prijmeni = value; OnPropertyChanged(nameof(Prijmeni)); } }

        private string _telefon;

        public string Telefon { get { return _telefon; } set { _telefon = value; OnPropertyChanged(nameof(Telefon)); } }

        private string _email;
        public string Email { get { return _email; } set { _email = value; OnPropertyChanged(nameof(Email)); } }
    }
}
