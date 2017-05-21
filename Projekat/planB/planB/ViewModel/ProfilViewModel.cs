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
        public ICommand PrikaziDnevnik { get; set; }

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
            Naziv = korisnik.Ime + " " + korisnik.Prezime;

            PrikaziDnevnik = new RelayCommand<object>(prikaziDnevnik);
        }

        private void prikaziDnevnik(object obj)
        {
            ProfilPage.frame.Navigate(typeof(DnevnikPage));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}