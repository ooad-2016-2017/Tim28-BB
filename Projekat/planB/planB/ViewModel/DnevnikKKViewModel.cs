using planB.Models;
using planB.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace planB.ViewModel
{
    class DnevnikKKViewModel : INotifyPropertyChanged
    {
        public List<StavkaDnevnika> lbxDnevnik { get; set; }
        public ICommand DodajDnevnik { get; set; }
        public String DatumText { get; set; }
        public String TextDnevnika { get; set; }
        Korisnik korisnik;
        public MessageDialog Poruka { get; set; }
        private StavkaDnevnika stavka;

        public event PropertyChangedEventHandler PropertyChanged;

        public DnevnikKKViewModel()
        {
            korisnik = LoginViewModel.korisnik;
            lbxDnevnik = korisnik.Dnevnik;
            DatumText = "";
            TextDnevnika = "";
            DodajDnevnik = new RelayCommand<object>(dodajDnevnikStavku);
            stavka = null;
            PregledVisibility = Visibility.Visible;
            UnosVisibility = Visibility.Collapsed;
            unos = Visibility.Collapsed;
            pregled = Visibility.Visible;
        }

        private Visibility unos;
        public Visibility UnosVisibility
        {
            get { return unos; }
            set
            {
                unos = value;
                if (unos == Visibility.Visible)
                {
                    PregledVisibility = Visibility.Collapsed;
                }
                NotifyPropertyChanged(nameof(UnosVisibility));
            }
        }

        private Visibility pregled;
        public Visibility PregledVisibility
        {
            get { return pregled; }
            set
            {
                pregled = value;
                NotifyPropertyChanged(nameof(PregledVisibility));
            }
        }

        public StavkaDnevnika PromjenaIndexa
        {
            get { return stavka; }

            set
            {
                stavka = value;
                if (stavka != null)
                {
                    TextDnevnika = stavka.ToString();
                    String datum = stavka.Datum.Date.ToString("dd.mm.yyyy.");
                    DatumText = datum;
                    PregledVisibility = Visibility.Visible;
                    UnosVisibility = Visibility.Collapsed;
                }
                NotifyPropertyChanged(nameof(PromjenaIndexa));
            }
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void dodajDnevnikStavku(object obj)
        {
            //ProfilPage.frame.Navigate(typeof())
        }
    }
}
