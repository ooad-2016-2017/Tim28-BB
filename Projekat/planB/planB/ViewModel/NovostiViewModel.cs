using planB.DBModels;
using planB.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace planB.ViewModel
{
    public class NovostiViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Korisnik TrenutniKorisnik;

        public ObservableCollection<Stavka> Stavke { get; set; }
        public ObservableCollection<MuzickaKolekcija> Kolekcije { get; set; }

        private Visibility stavkeVisibility;
        private Visibility kolekcijeVisibility;

        public ICommand StavkeNovosti { get; set; }
        public ICommand KolekcijeNovosti { get; set; }

        List<Follow> followLista;
        MuzickaKolekcijaViewModel muzickaKolekcijaViewModel;

        public NovostiViewModel()
        {
            TrenutniKorisnik = LoginViewModel.korisnik;
            Stavke = new ObservableCollection<Stavka>();
            Kolekcije = new ObservableCollection<MuzickaKolekcija>();
            KolekcijeVisibility = Visibility.Collapsed;
            StavkeVisibility = Visibility.Visible;

            povuciFollowDetalje();
            povuciStavke();
            povuciKolekcije();

            StavkeNovosti = new RelayCommand<object>(prikaziStavke);
            KolekcijeNovosti = new RelayCommand<object>(prikaziKolekcije);
            
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public Visibility StavkeVisibility
        {
            get { return stavkeVisibility; }
            set
            {
                stavkeVisibility = value;
                NotifyPropertyChanged(nameof(StavkeVisibility));
            }
        }

        public Visibility KolekcijeVisibility
        {
            get { return kolekcijeVisibility; }
            set
            {
                kolekcijeVisibility = value;
                NotifyPropertyChanged(nameof(KolekcijeVisibility));
            }
        }

        private void povuciFollowDetalje()
        {
            using (var DB = new PlanBDbContext())
            {
                followLista = DB.Follow.Where(x => (x.KorisnikID == TrenutniKorisnik.ID)).ToList();
            }
        }

        private void povuciStavke()
        {
            using (var DB = new PlanBDbContext())
            {
                foreach(Follow f in followLista)
                {
                    foreach(Obaveza o in DB.Obaveze)
                    {
                        if (o.KreatorID == f.Following_KorisnikID)
                            Stavke.Add(o);
                    }
                    foreach(StavkaDnevnika sd in DB.Dnevnik)
                    {
                        if (sd.KreatorID == f.Following_KorisnikID)
                            Stavke.Add(sd);
                    }
                }
            }
        }

        private void povuciKolekcije()
        {
            using (var DB = new PlanBDbContext())
            {
                foreach(Follow f in followLista)
                {
                   foreach(MuzickaKolekcija mk in DB.MuzickaKolekcija)
                    {
                        if (mk.KorisnikID == f.Following_KorisnikID)
                        {
                            Kolekcije.Add(mk);
                        }
                    }
                }
            }
        }

        private void prikaziStavke(object parametar)
        {
            StavkeVisibility = Visibility.Visible;
            KolekcijeVisibility = Visibility.Collapsed;
        }

        private void prikaziKolekcije(object parametar)
        {
            StavkeVisibility = Visibility.Collapsed;
            KolekcijeVisibility = Visibility.Visible;
        }

    }
}
