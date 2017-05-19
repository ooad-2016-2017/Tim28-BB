using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.Models
{
    public class Korisnik : INotifyPropertyChanged
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        int id;
        String ime;
        String prezime;
        String korisnickoIme;
        String lozinka;
        DateTime datumRodjenja;
        String email;
        byte[] slika;

        List<Obaveza> obaveze;
        List<StavkaDnevnika> dnevnik;
        List<MuzickaKolekcija> muzickaKolekcija;

        public event PropertyChangedEventHandler PropertyChanged;

        public Korisnik() { }
        public Korisnik(int _id, String _ime, String _prezime, String _korisnickoIme, String _lozinka, DateTime _datumRodjenja, String _email, byte[] _slika = null)
        {
            id = _id;
            ime = _ime;
            prezime = _prezime;
            korisnickoIme = _korisnickoIme;
            lozinka = _lozinka;
            datumRodjenja = _datumRodjenja;
            email = _email;
            slika = _slika;
            obaveze = new List<Obaveza>();
            dnevnik = new List<StavkaDnevnika>();
            muzickaKolekcija = new List<MuzickaKolekcija>();
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged(nameof(ID));
            }
        }

        public String Ime
        {
            get { return ime; }
            set
            {
                ime = value;
                NotifyPropertyChanged(nameof(Ime));
            }
        }

        public String Prezime
        {
            get { return prezime; }
            set
            {
                prezime = value;
                NotifyPropertyChanged(nameof(Prezime));
            }
        }

        public String KorisnickoIme
        {
            get { return korisnickoIme; }
            set
            {
                korisnickoIme = value;
                NotifyPropertyChanged(nameof(KorisnickoIme));
            }
        }

        public String Lozinka
        {
            get { return lozinka; }
            set
            {
                lozinka = value;
                NotifyPropertyChanged(nameof(Lozinka));
            }
        }

        public DateTime DatumRodjenja
        {
            get { return datumRodjenja; }
            set
            {
                datumRodjenja = value;
                NotifyPropertyChanged(nameof(DatumRodjenja));
            }
        }

        public String Email
        {
            get { return email; }
            set
            {
                email = value;
                NotifyPropertyChanged(nameof(Email));
            }
        }

        public byte[] Slika
        {
            get { return slika; }
            set
            {
                slika = value;
                NotifyPropertyChanged(nameof(Slika));
            }
        }

        public List<Obaveza> Obaveze
        {
            get { return obaveze; }
            set
            {
                obaveze = value;
                NotifyPropertyChanged(nameof(Obaveze));
            }
        }

        public List<StavkaDnevnika> Dnevnik
        {
            get { return dnevnik; }
            set
            {
                dnevnik = value;
                NotifyPropertyChanged(nameof(Dnevnik));
            }
        }

        public List<MuzickaKolekcija> MuzickaKolekcija
        {
            get { return muzickaKolekcija; }
            set
            {
                muzickaKolekcija = value;
                NotifyPropertyChanged(nameof(MuzickaKolekcija));
            }
        }

    }
}
