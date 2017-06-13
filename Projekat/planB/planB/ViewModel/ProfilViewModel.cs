using Microsoft.WindowsAzure.MobileServices;
using planB.AzureModels;
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

        private PorukeViewModel pwm;

        public String Naziv { get; set; }
        public Korisnik korisnik { get; set; }
        private int brojNovihPoruka;

        public ICommand PrikaziDnevnik { get; set; }
        public ICommand PrikaziMuzickuKolekciju { get; set; }
        public ICommand PrikaziLokaciju { get; set; }
        public ICommand PrikaziPoruke { get; set; }

        public String Pretraga { get; set; }
        private ObservableCollection<Korisnik> rezultatiPretrage;

        private bool vidljivost;

        public ProfilViewModel()
        {
            DnevnikVisibility = false;
            korisnik = LoginViewModel.korisnik;
            Naziv = korisnik.Ime + " " + korisnik.Prezime;

            rezultatiPretrage = new ObservableCollection<Korisnik>();
            pwm = new PorukeViewModel();
            BrojNovihPoruka = pwm.BrojNovihPoruka;
            PrikaziDnevnik = new RelayCommand<object>(prikaziDnevnik);
            PrikaziMuzickuKolekciju = new RelayCommand<object>(prikaziMuzickuKolekciju);
            PrikaziLokaciju = new RelayCommand<object>(prikaziLokaciju);
            PrikaziPoruke = new RelayCommand<object>(prikaziPoruke);
        }

        public bool DnevnikVisibility
        {
            get { return vidljivost; }
            set
            {
                vidljivost = value;
                NotifyPropertyChanged(nameof(DnevnikVisibility));
            }
        }

        public int BrojNovihPoruka
        {
            get { return brojNovihPoruka; }
            set
            {
                brojNovihPoruka = value;
                NotifyPropertyChanged(nameof(BrojNovihPoruka));
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
            //ProfilPage.frame.Navigate(typeof(DnevnikPage));
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
            //ProfilPage.frame.Navigate(typeof(MuzickaKolekcijaPage), new MuzickaKolekcijaViewModel());
        }

        private void prikaziLokaciju(object parametar)
        {
            ProfilPage.frame.Navigate(typeof(LocationPage));
        }

        private async void prikaziPoruke(object parametar)
        {
            using (var DB = new PlanBDbContext())
            {
                IMobileServiceTable<PorukaAzure> porukeAzure = App.MobileService.GetTable<PorukaAzure>();
                int broj = DB.Poruke.Count();
                List<PorukaAzure> listaPoruke = await porukeAzure.Where(x => x.redniBroj > 0).ToListAsync();
                if (broj < listaPoruke.Count)
                {
                    listaPoruke = await porukeAzure.Where(x => x.redniBroj > broj).ToListAsync();

                    foreach (PorukaAzure o in listaPoruke)
                    {

                        using (var DB1 = new PlanBDbContext())
                        {
                            Poruka poruka = new Poruka(o.tekst, o.datumSlanja, o.posiljaocID, o.primaocID, o.dajStatus());
                            poruka.idAzure = o.id;
                            DB1.Poruke.Add(poruka);
                            DB1.SaveChanges();
                        }
                    }
                }
            }
            ProfilPage.frame.Navigate(typeof(PorukaPage));
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
                //Poruka = new MessageDialog("Ne postoji korisnik sa unesenim podacima.");
                //await Poruka.ShowAsync();
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