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
    public class PjesmaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MessageDialog Poruka { get; set; }
        private MuzickaKolekcijaViewModel muzickaKolekcijaViewModel;

        public Pjesma OdabranaPjesma { get; set; }
        private String pjesmaDetails;
        public String NazivNoveKolekcije { get; set; }

        public ICommand PustiPjesmu { get; set; }
        public ICommand DodajUKolekciju { get; set; }
        public ICommand ZavrsiDodavanje { get; set; }
        public ICommand SelectionChanged { get; set; }

        private Visibility dodajVisiblity;
        private Visibility nazivKolekcijeVisibility;
        private Visibility pjesmaDetailsVisibility;
        public MuzickaKolekcija odabranaKolekcija;

        public ObservableCollection<MuzickaKolekcija> MuzKolekcija { get; set; }


        public Korisnik TrenutniKorisnik { get; set; }

        public PjesmaViewModel()
        {
            pjesmaDetails = "";
            PustiPjesmu = new RelayCommand<object>(pustiPjesmu);
        }

        public PjesmaViewModel(Pjesma odabranaPjesma, Korisnik trenutniKorisnik)
        {
            OdabranaPjesma = odabranaPjesma;
            pjesmaDetails = odabranaPjesma.Izvodjac + " - " + odabranaPjesma.Naziv;
            TrenutniKorisnik = trenutniKorisnik;
            NazivNoveKolekcije = "";
            dodajVisiblity = Visibility.Collapsed;
            nazivKolekcijeVisibility = Visibility.Collapsed;
            pjesmaDetailsVisibility = Visibility.Visible;
            muzickaKolekcijaViewModel = new MuzickaKolekcijaViewModel();

            MuzKolekcija = new ObservableCollection<MuzickaKolekcija>(TrenutniKorisnik.MuzickaKolekcija);
            MuzKolekcija.Add(new MuzickaKolekcija(0, "Nova kolekcija...", null)); // ispis........


            PustiPjesmu = new RelayCommand<object>(pustiPjesmu);
            DodajUKolekciju = new RelayCommand<object>(dodajUKolekciju);
            ZavrsiDodavanje = new RelayCommand<object>(zavrsiDodavanje);
        }

        public String PjesmaDetails
        {
            get { return pjesmaDetails; }
            set
            {
                pjesmaDetails = value;
                NotifyPropertyChanged(nameof(PjesmaDetails));
            }
        }

        public Visibility DodajVisibility
        {
            get { return dodajVisiblity; }
            set
            {
                dodajVisiblity = value;
                NotifyPropertyChanged(nameof(DodajVisibility));
            }
        }

        public Visibility NazivKolekcijeVisibility
        {
            get { return nazivKolekcijeVisibility; }
            set
            {
                nazivKolekcijeVisibility = value;
                NotifyPropertyChanged(nameof(NazivKolekcijeVisibility));
            }
        }

        public Visibility PjesmaDetailsVisibility
        {
            get { return pjesmaDetailsVisibility; }
            set
            {
                pjesmaDetailsVisibility = value;
                NotifyPropertyChanged(nameof(PjesmaDetailsVisibility));
            }
        }

        public MuzickaKolekcija OdabranaKolekcija
        {
            get { return odabranaKolekcija; }
            set
            {

                odabranaKolekcija = value;

                if (odabranaKolekcija.Naziv == "Nova kolekcija...")
                {
                    NazivKolekcijeVisibility = Visibility.Visible;
                }
                else
                {
                    muzickaKolekcijaViewModel.dodajUPostojecu(odabranaKolekcija, OdabranaPjesma);
                    PjesmaDetailsVisibility = Visibility.Visible;
                    DodajVisibility = Visibility.Collapsed;
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

        private void pustiPjesmu(object parametar)
        {
            PrikazPjesmeForm.PlaySound.Play();
        }

        private  void dodajUKolekciju(object parametar)
        {
            DodajVisibility = Visibility.Visible;
            PjesmaDetailsVisibility = Visibility.Collapsed; 
        }

        private void zavrsiDodavanje(object parametar)
        {
            muzickaKolekcijaViewModel.dodajNovuKolekciju(NazivNoveKolekcije, OdabranaPjesma);

            NazivKolekcijeVisibility = Visibility.Collapsed;
            DodajVisibility = Visibility.Collapsed;
            PjesmaDetailsVisibility = Visibility.Visible;
        }       
    } 
}
