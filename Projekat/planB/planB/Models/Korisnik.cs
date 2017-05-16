using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.Models
{
    public class Korisnik : INotifyPropertyChanged
    {
        String ime;
        String prezime;
        String korisnickoIme;
        String lozinka;
        DateTime datumRodjenja;
        String email;
        byte[] slika;

        public event PropertyChangedEventHandler PropertyChanged;

        public Korisnik() { }
        public Korisnik(String _ime, String _prezime, String _korisnickoIme, String _lozinka, DateTime _datumRodjenja, String _email, byte[] _slika = null)
        {
            ime = _ime;
            prezime = _prezime;
            korisnickoIme = _korisnickoIme;
            lozinka = _lozinka;
            datumRodjenja = _datumRodjenja;
            email = _email;
            slika = _slika;
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
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

    }
}
