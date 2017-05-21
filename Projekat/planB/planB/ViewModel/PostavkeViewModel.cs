using planB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace planB.ViewModel
{
    class PostavkeViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MessageDialog Poruka { get; set; }
        public String ime;
        //public ICommand PromjenaDatuma { get; set; }
        public ICommand PotvrdiButton { get; set; }
        DateTime datum;
        public String prezime;
        public String stara;
        public String nova;

        Korisnik korisnik { get; set; }

        public String imeTbx {
            get { return ime; }
            set
            {
                if (value != ime)
                {
                    ime = value;
                    NotifyPropertyChanged(nameof(imeTbx));
                }
            }
        }

        public String prezimeTbx {
            get { return prezime; }
            set
            {
                if (value != prezime)
                {
                    prezime = value;
                    NotifyPropertyChanged(nameof(prezimeTbx));
                }
            }
        }
        public String staraLozinka {
            get { return stara; }
            set
            {
                if (value != stara)
                {
                    stara = value;
                    NotifyPropertyChanged(nameof(staraLozinka));
                }
            }
        }
        public String novaLozinka {
            get { return nova; }
            set
            {
                if (value != nova)
                {
                    nova = value;
                    NotifyPropertyChanged(nameof(novaLozinka));
                }
            }
        }

        public PostavkeViewModel()
        {
            korisnik = LoginViewModel.korisnik;
            imeTbx = korisnik.Ime;
            prezimeTbx = korisnik.Prezime;
            staraLozinka = korisnik.Lozinka;
            novaLozinka = "";
            datum = korisnik.DatumRodjenja;
            //PromjenaDatuma = new RelayCommand<object>(promjenaDatuma);
            PotvrdiButton = new RelayCommand<object>(potvrdiButton);
        }
        
        private async void potvrdiButton(object obj)
        {
            if(imeTbx.Length < 2 || prezimeTbx.Length < 3 || novaLozinka.Length < 3)
            {
                Poruka = new MessageDialog("Unesite sve tražene podatke.");
                await Poruka.ShowAsync();
                return;
            }
            using (var DB = new PlanBDbContext())
            {
                korisnik.Ime = imeTbx;
                korisnik.Prezime = prezimeTbx;
                korisnik.Lozinka = novaLozinka;
                korisnik.DatumRodjenja = datum;
                DB.Korisnici.Update(korisnik);
                DB.SaveChanges();// DB.Korisnici.Where(x => (x.KorisnickoIme == korisnik.KorisnickoIme)).FirstOrDefault());
                Poruka = new MessageDialog("Podaci uspješno izmijenjeni.");
                await Poruka.ShowAsync();
            }

        }



        private void NotifyPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

    }
}
