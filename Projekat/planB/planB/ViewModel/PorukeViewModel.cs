using planB.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace planB.ViewModel
{
    public class PorukeViewModel : INotifyPropertyChanged
    {
        public MessageDialog Poruka;
        public event PropertyChangedEventHandler PropertyChanged;

        public Korisnik TrenutniKorisnik;
        public ObservableCollection<Poruka> Poruke { get; set; }
        public List<Poruka> NeprocitanePoruke { get; set; }

        private Boolean unosPorukeVisibility;
        private Boolean pregledPorukeVisibility;
        private int brojNovihPoruka;
        private Poruka odabranaPoruka;

        private String sadrzajNovePoruke;
        public ICommand OdgovoriNaPoruku { get; set; }
        public ICommand PosaljiPoruku { get; set; }

        public Boolean UnosPorukeVisibility
        {
            get { return unosPorukeVisibility; }
            set
            {
                unosPorukeVisibility = value;
                NotifyPropertyChanged(nameof(UnosPorukeVisibility));
            }
        }

        public Boolean PregledPorukeVisibility
        {
            get { return pregledPorukeVisibility; }
            set
            {
                pregledPorukeVisibility = value;
                NotifyPropertyChanged(nameof(PregledPorukeVisibility));
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

        public Poruka OdabranaPoruka
        {
            get
            {
                //odabranaPoruka.StatusPoruke = StatusPoruke.Procitano;
                return odabranaPoruka;
            }
            set
            {
                using (var DB = new PlanBDbContext())
                {
                    value.StatusPoruke = StatusPoruke.Procitano;
                    DB.Poruke.Update(value);
                    DB.SaveChanges();
                    odabranaPoruka = value;
                }
                NotifyPropertyChanged(nameof(OdabranaPoruka));
            }
        }

        public String SadrzajNovePoruke
        {
            get { return sadrzajNovePoruke; }
            set
            {
                sadrzajNovePoruke = value;
                NotifyPropertyChanged(nameof(SadrzajNovePoruke));
            }
        }

        public PorukeViewModel()
        {
            BrojNovihPoruka = 0;
            TrenutniKorisnik = LoginViewModel.korisnik;
            UnosPorukeVisibility = false;
            PregledPorukeVisibility = true;
            SadrzajNovePoruke = "";

            OdgovoriNaPoruku = new RelayCommand<object>(odgovoriNaPoruku);
            PosaljiPoruku = new RelayCommand<object>(posaljiPoruku);
            povuciPoruke();
        }

        

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void odgovoriNaPoruku(object parametar)
        {

            PregledPorukeVisibility = false;
            UnosPorukeVisibility = true;
        }

        private async void posaljiPoruku(object parametar)
        {
            if (SadrzajNovePoruke.Length > 0)
            {
                using (var DB = new PlanBDbContext())
                {
                    Poruka novaPoruka = new Poruka();
                    novaPoruka.Tekst = SadrzajNovePoruke;
                    novaPoruka.PosiljaocID = TrenutniKorisnik.ID;
                    novaPoruka.PrimaocID = OdabranaPoruka.PosiljaocID;
                    novaPoruka.StatusPoruke = StatusPoruke.Neprocitano;
                    novaPoruka.DatumSlanja = DateTime.Today;
                    DB.Poruke.Add(novaPoruka);
                    DB.SaveChanges();
                }

                Poruka = new MessageDialog("Poruka uspješno poslana.");
                await Poruka.ShowAsync();
                UnosPorukeVisibility = false;
                PregledPorukeVisibility = true;
            }
            else
            {
                Poruka = new MessageDialog("Ne možete poslati praznu poruku.");
                await Poruka.ShowAsync();
                return;
            }
        }

        private void povuciPoruke()
        {
            List<Poruka> poruke = new List<Poruka>();
            using (var DB = new PlanBDbContext())
            {
                poruke = DB.Poruke.Where(x => (x.PrimaocID == TrenutniKorisnik.ID)).ToList();
                NeprocitanePoruke = DB.Poruke.Where(x => (x.PrimaocID == TrenutniKorisnik.ID && x.StatusPoruke == StatusPoruke.Neprocitano)).ToList();
            }
            poruke.Sort((x, y) => DateTime.Compare(x.DatumSlanja, y.DatumSlanja));
            Poruke = new ObservableCollection<Poruka>(poruke);
            BrojNovihPoruka = NeprocitanePoruke.Count;
        }
    }
}
