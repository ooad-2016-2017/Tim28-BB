using planB.Models;
using planB.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace planB.ViewModel
{
    class ProfilViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MessageDialog Poruka;

        public String Naziv { get; set; }
        public Korisnik korisnik { get; set; }
        public ICommand PrikaziDnevnik { get; set; }

        public ICommand PrikaziMuzickuKolekciju { get; set; }

        public String Pretraga { get; set; }
        private ObservableCollection<Korisnik> rezultatiPretrage;

        private Visibility vidljivost;

        public ProfilViewModel()
        {
            DnevnikVisibility = Visibility.Collapsed;

            korisnik = LoginViewModel.korisnik;
            Naziv = korisnik.Ime + " " + korisnik.Prezime;

            rezultatiPretrage = new ObservableCollection<Korisnik>();

            PrikaziDnevnik = new RelayCommand<object>(prikaziDnevnik);
            PrikaziMuzickuKolekciju = new RelayCommand<object>(prikaziMuzickuKolekciju);
        }

        public Visibility DnevnikVisibility
        {
            get { return vidljivost; }
            set
            {
                vidljivost = value;
                NotifyPropertyChanged(nameof(DnevnikVisibility));
            }
        }

        public ObservableCollection<Korisnik> RezultatiPretrage
        {
            get { return rezultatiPretrage; }
            set
            {
                rezultatiPretrage = value;
                NotifyPropertyChanged(nameof(RezultatiPretrage));
            }
        }

        private void prikaziDnevnik(object obj)
        {
            ProfilPage.frame.Navigate(typeof(DnevnikPage));
        }


        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void prikaziMuzickuKolekciju(object parametar)
        {
            ProfilPage.frame.Navigate(typeof(MuzickaKolekcijaPage), new MuzickaKolekcijaViewModel(LoginViewModel.korisnik));
        }

        public async void PronadjiKorisnika()
        {
            await pronadjiKorisnika();

        }

        private async Task pronadjiKorisnika()
        {
            rezultatiPretrage = new ObservableCollection<Korisnik>();
            List<Korisnik> korisnici = new List<Korisnik>();
            using (var DB = new PlanBDbContext())
            {
                korisnici = DB.Korisnici.Where(x => ((x.Ime.ToString() + " " + x.Prezime.ToString()) == Pretraga)).ToList();
            }
            RezultatiPretrage = new ObservableCollection<Korisnik>(korisnici);
            if (rezultatiPretrage.Count == 0)
            {
                Poruka = new MessageDialog("Ne postoji korisnik sa unesenim podacima.");
                await Poruka.ShowAsync();
            }
        }

        public void PretraziKorisnike()
        {
            List<Korisnik> korisnici = new List<Korisnik>();
            using (var DB = new PlanBDbContext())
            {
                korisnici = DB.Korisnici.Where(x => (x.Ime.Contains(Pretraga) || x.Prezime.Contains(Pretraga))).ToList();
            }
            RezultatiPretrage = new ObservableCollection<Korisnik>(korisnici);
        }

       

        public void PrikaziProfilKorisnika(Korisnik odabraniKorisnik)
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(PregledProfilaKorisnika), new PregledProfilaKorisnikaViewModel(odabraniKorisnik)); 
        }
    }
}