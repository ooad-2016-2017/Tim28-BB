using planB.DBModels;
using planB.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace planB.ViewModel
{
    public class NovostiViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Korisnik TrenutniKorisnik;

        public ObservableCollection<Stavka> Stavke { get; set; }
        public ObservableCollection<MuzickaKolekcija> Kolekcije { get; set; }

        private Boolean stavkeVisibility;
        private Boolean kolekcijeVisibility;

        public ICommand StavkeNovosti { get; set; }
        public ICommand KolekcijeNovosti { get; set; }

        List<Follow> followLista;

        public NovostiViewModel()
        {
            TrenutniKorisnik = LoginViewModel.korisnik;
            Stavke = new ObservableCollection<Stavka>();
            Kolekcije = new ObservableCollection<MuzickaKolekcija>();
            KolekcijeVisibility = false;
            StavkeVisibility = true;

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

        public Boolean StavkeVisibility
        {
            get { return stavkeVisibility; }
            set
            {
                stavkeVisibility = value;
                NotifyPropertyChanged(nameof(StavkeVisibility));
            }
        }

        public Boolean KolekcijeVisibility
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
                followLista = DB.Follow.Where(x => (x.KorisnikID == TrenutniKorisnik.idAzure)).ToList();
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
                        if (o.kreatorAzure == f.Following_KorisnikID)
                            Stavke.Add(o);
                    }
                    foreach(StavkaDnevnika sd in DB.Dnevnik)
                    {
                        if (sd.kreatorAzure == f.Following_KorisnikID)
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
                        if (mk.idAzure == f.Following_KorisnikID)
                        {
                            Kolekcije.Add(mk);
                        }
                    }
                }
            }
        }

        private void prikaziStavke(object parametar)
        {
            StavkeVisibility = true;
            KolekcijeVisibility = false;
        }

        private void prikaziKolekcije(object parametar)
        {
            StavkeVisibility = false;
            KolekcijeVisibility = true;
        }

    }
}
