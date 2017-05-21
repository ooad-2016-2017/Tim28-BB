using planB.Models;
using planB.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace planB.ViewModel
{
    class ProfilViewModel : INotifyPropertyChanged
    {
        public String Naziv { get; set; }
        public Korisnik korisnik { get; set; }
        
        public ICommand PrikaziMuzickuKolekciju { get; set; }

        private Visibility vidljivost;
        public Visibility DnevnikVisibility
        {
            get { return vidljivost; }
            set
            {
                vidljivost = value;
                NotifyPropertyChanged(nameof(DnevnikVisibility));
            }
        }


        public ProfilViewModel()
        {
            DnevnikVisibility = Visibility.Collapsed;
            korisnik = LoginViewModel.korisnik;
            Naziv = korisnik.Ime + korisnik.Prezime;
            PrikaziMuzickuKolekciju = new RelayCommand<object>(prikaziMuzickuKolekciju);
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
    }
}
