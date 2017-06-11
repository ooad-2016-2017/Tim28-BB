using planB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using planB.View;
using planB.DBModels;
using Microsoft.WindowsAzure.MobileServices;
using planB.AzureModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace planB.ViewModel
{
    public class PregledProfilaKorisnikaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MessageDialog Poruka;

        public Korisnik TrenutniKorisnik { get; set; }
        public Korisnik OdabraniKorisnik { get; set; }
        public String ImeIPrezime { get; set; } // jer su u jednom TextBlock-u...
        public String TekstPoruke { get; set; }

        public ICommand ZapratiKorisnika { get; set; }
        public ICommand PrikaziMuzickuKolekciju { get; set; }
        public ICommand PrikaziObaveze { get; set; }
        public ICommand PosaljiPoruku { get; set; }
        public ICommand ZavrsiSlanjePoruke { get; set; }
        public ICommand VracanjeNazad { get; set; }

        private String followStatus;
        private int followNumber;
        private int followingNumber;

        private Boolean unosPorukeVisibility;

        public PregledProfilaKorisnikaViewModel()
        {
            
        }

        public PregledProfilaKorisnikaViewModel(Korisnik _OdabraniKorisnik)
        {
            OdabraniKorisnik = _OdabraniKorisnik;
            TrenutniKorisnik = LoginViewModel.korisnik;
            ImeIPrezime = OdabraniKorisnik.Ime + " " + OdabraniKorisnik.Prezime;
            UnosPorukeVisibility = false;

            FollowNumber = 0;
            FollowingNumber = 0;
            povuciFollowingDetalje();

            provjeriFollowStatus();
            PrikaziMuzickuKolekciju = new RelayCommand<object>(prikaziMuzickuKolekciju);
            ZapratiKorisnika = new RelayCommand<object>(zapratiKorisnika);
            PrikaziObaveze = new RelayCommand<object>(prikaziObaveze);
            PosaljiPoruku = new RelayCommand<object>(posaljiPoruku);
            ZavrsiSlanjePoruke = new RelayCommand<object>(zavrsiSlanje);
            VracanjeNazad = new RelayCommand<object>(vratiNazad);
        }

        public Boolean UnosPorukeVisibility
        {
            get { return unosPorukeVisibility; }
            set
            {
                unosPorukeVisibility = value;
                NotifyPropertyChanged(nameof(UnosPorukeVisibility));
            }
        }

        public String FollowStatus
        {
            get { return followStatus; }
            set
            {
                followStatus = value;
                NotifyPropertyChanged(nameof(FollowStatus));
            }
        }

        public int FollowNumber
        {
            get { return followNumber; }
            set
            {
                followNumber = value;
                NotifyPropertyChanged(nameof(FollowNumber));
            }
        }

        public int FollowingNumber
        {
            get { return followingNumber; }
            set
            {
                followingNumber = value;
                NotifyPropertyChanged(nameof(FollowingNumber));
            }
        }

        private void provjeriFollowStatus()
        {
            using (var DB = new PlanBDbContext())
            {
                Follow followCheck = DB.Follow.Where(x => (x.KorisnikID == TrenutniKorisnik.idAzure && x.Following_KorisnikID == OdabraniKorisnik.idAzure)).FirstOrDefault();
                if (followCheck != null)
                    FollowStatus = "Pratim";
                else
                    FollowStatus = "Prati";
            }
        }

        private void posaljiPoruku(object parametar)
        {
            UnosPorukeVisibility = true;
        }

        private async void zavrsiSlanje(object parametar)
        {
            if (TekstPoruke.Length > 0)
            {
                using (var DB = new PlanBDbContext())
                {
                    IMobileServiceTable<PorukaAzure> userTableObj = App.MobileService.GetTable<PorukaAzure>();
                    PorukaAzure poruka = new PorukaAzure();
                    poruka.postaviStatus(StatusPoruke.Neprocitano);
                    poruka.posiljaocID = TrenutniKorisnik.idAzure;
                    poruka.primaocID = OdabraniKorisnik.idAzure;
                    poruka.tekst = TekstPoruke;
                    poruka.datumSlanja = DateTime.Today;
                    IMobileServiceTable<PorukaAzure> azureObaveze = App.MobileService.GetTable<PorukaAzure>();
                    List<PorukaAzure> listaAzure = await azureObaveze.Where(x => x.id != "").ToListAsync();
                    poruka.redniBroj = listaAzure.Count + 1;
                    await userTableObj.InsertAsync(poruka);

                    Poruka novaPoruka = new Poruka();
                    novaPoruka.Tekst = TekstPoruke;
                    novaPoruka.posiljaocAzure = poruka.posiljaocID;
                    novaPoruka.primaocAzure = poruka.primaocID;
                    novaPoruka.StatusPoruke = StatusPoruke.Neprocitano;
                    novaPoruka.DatumSlanja = DateTime.Today;
                    DB.Poruke.Add(novaPoruka);
                    DB.SaveChanges();
                }
                Poruka = new MessageDialog("Poruka uspješno poslana.");
                await Poruka.ShowAsync();
                UnosPorukeVisibility = false;
            }
            else
            {
                Poruka = new MessageDialog("Ne možete poslati praznu poruku.");
                await Poruka.ShowAsync();
                return;
            }
        }

        private async void zapratiKorisnika(object parametar)
        {
            using (var DB = new PlanBDbContext())
            {
                if (FollowStatus == "Prati")
                {
                    IMobileServiceTable<FollowAzure> userTableObj = App.MobileService.GetTable<FollowAzure>();
                    FollowAzure poruka = new FollowAzure();
                    poruka.korisnikID = TrenutniKorisnik.idAzure;
                    poruka.following_KorisnikID = OdabraniKorisnik.idAzure;
                    IMobileServiceTable<FollowAzure> azureObaveze = App.MobileService.GetTable<FollowAzure>();
                    List<FollowAzure> listaAzure = await azureObaveze.Where(x => x.korisnikID != "").ToListAsync();
                    poruka.redniBroj = listaAzure.Count + 1;
                    await userTableObj.InsertAsync(poruka);


                    Follow newFollow = new Follow();
                    newFollow.KorisnikID = TrenutniKorisnik.idAzure;
                    newFollow.Following_KorisnikID = OdabraniKorisnik.idAzure;
                    FollowStatus = "Pratim";
                    DB.Follow.Add(newFollow);
                    DB.SaveChanges();
                    Poruka = new MessageDialog("Korisnik uspješno zapraćen.\nSada možete zajedno planirati i dijeliti vašu omiljenu muziku. :)");
                    await Poruka.ShowAsync();
                }
                else
                {
                    Poruka = new MessageDialog("Korisnik je već zapraćen.");
                    await Poruka.ShowAsync();
                }
            }
           
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void povuciFollowingDetalje()
        {
            using (var DB = new PlanBDbContext())
            {
                List<Follow> follow = DB.Follow.Where(x => (x.KorisnikID == OdabraniKorisnik.idAzure)).ToList();
                FollowingNumber = follow.Count();
                follow = DB.Follow.Where(x => (x.Following_KorisnikID == OdabraniKorisnik.idAzure)).ToList();
                FollowNumber = follow.Count();
            }
        }

        private void prikaziMuzickuKolekciju(object parametar)
        {
            PregledProfilaKorisnika.frame.Navigate(typeof(MuzickaKolekcijaPage), new MuzickaKolekcijaViewModel(OdabraniKorisnik));
        }

        private void prikaziObaveze(object parametar)
        {
            PregledProfilaKorisnika.frame.Navigate(typeof(PregledObaveza), new PregledObavezaViewModel(OdabraniKorisnik.idAzure));
        }

        private void vratiNazad(object parametar)
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(ProfilPage));
        }
    }

}
