using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.AzureModels
{
    class KorisnikAzure
    {
        public String id { get; set; }
        public String ime { get; set; }
        public String prezime { get; set; }
        public String korisnickoIme { get; set; }
        public String lozinka { get; set; }
        public DateTime datumRodjenja { get; set; }
        public String email { get; set; }
        public int redniBroj { get; set; }
        //public byte[] slika { get; set; }

        public KorisnikAzure() { }
        public KorisnikAzure(String _id, String _ime, String _prezime, String _korisnickoIme, String _lozinka, DateTime _datumRodjenja, String _email, byte[] _slika = null)
        {
            id = _id;
            ime = _ime;
            prezime = _prezime;
            korisnickoIme = _korisnickoIme;
            lozinka = _lozinka;
            datumRodjenja = _datumRodjenja;
            email = _email;
            //slika = _slika;
        }
    }
}
