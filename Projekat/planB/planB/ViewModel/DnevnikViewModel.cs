using Microsoft.WindowsAzure.MobileServices;
using planB.AzureModels;
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
using Windows.UI.Xaml.Navigation;

namespace planB.ViewModel
{
    class DnevnikViewModel : INotifyPropertyChanged
    {

        IMobileServiceTable<StavkaDnevnikAzure> userTableObj = App.MobileService.GetTable<StavkaDnevnikAzure>();

        public List<StavkaDnevnika> lbxDnevnik { get; set; }
        public ICommand DodajDnevnik { get; set; }
        public String DatumText { get; set; }
        public String TextDnevnika { get; set; }
        Korisnik korisnik;
        public MessageDialog Poruka { get; set; }
        private StavkaDnevnika stavka;


        public Vidljivost vidljivost;
        private bool privatno;
        private bool javno;
        public ICommand AddButtonClicked { get; set; }
        public String UnosDnevnikaTextBox { get; set; }
        public List<StavkaDnevnika> lbxItems { get; set; }
        public String NaslovTextBox { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public DnevnikViewModel()
        {
            korisnik = LoginViewModel.korisnik;
            lbxDnevnik = korisnik.Dnevnik;
            DatumText = "";
            TextDnevnika = "";
            NaslovTextBox = "";
            DodajDnevnik = new RelayCommand<object>(dodajDnevnikStavku);
            stavka = null;
            PregledVisibility = Visibility.Visible;
            UnosVisibility = Visibility.Collapsed;
            unos = Visibility.Collapsed;
            pregled = Visibility.Visible;
            UnosVisibility = Visibility.Collapsed;
            PregledVisibility = Visibility.Visible;
            UnosDnevnikaTextBox = "";
            lbxItems = new List<StavkaDnevnika>();
            vidljivost = Vidljivost.Nista;

            AddButtonClicked = new RelayCommand<object>(addButtonClicked);
            for (int i = 0; i < korisnik.Dnevnik.Count(); i++)
                lbxItems.Add(korisnik.Dnevnik[i]);
        }

        private async void addButtonClicked(object obj)
        {
            using (var DB = new PlanBDbContext())
            {

                if (UnosDnevnikaTextBox.Length < 3)
                {
                    Poruka = new MessageDialog("Unesite tekst.");
                    await Poruka.ShowAsync();
                    return;
                }

                if(NaslovTextBox.Length < 3)
                {
                    Poruka = new MessageDialog("Unesite naslov.");
                    await Poruka.ShowAsync();
                    return;
                }

                if (vidljivost == Vidljivost.Nista)
                {
                    Poruka = new MessageDialog("Unesite vidljivost.");
                    await Poruka.ShowAsync();
                    return;
                }

                //new DateTime(int.Parse(String.Concat(DatumText[6] + DatumText[7] + DatumText[8] + DatumText[9])), int.Parse(String.Concat(DatumText[3] + DatumText[4])), int.Parse(String.Concat(DatumText[0] + DatumText[1])))

                StavkaDnevnikAzure stavka = new StavkaDnevnikAzure();
                stavka.datum = DateTime.Now;
                stavka.kreatorID = korisnik.idAzure;
                stavka.naslov = NaslovTextBox;
                stavka.sadrzaj = UnosDnevnikaTextBox;
                stavka.postaviVidljivost(vidljivost);
                await userTableObj.InsertAsync(stavka);

                StavkaDnevnika sd = new StavkaDnevnika(0, DateTime.Now, UnosDnevnikaTextBox, vidljivost, NaslovTextBox, korisnik.idAzure);
                sd.kreatorAzure = stavka.id;
                korisnik.Dnevnik.Add(sd);
                DB.Dnevnik.Add(sd);
                DB.SaveChanges();
                lbxItems.Add(sd);
                Poruka = new MessageDialog("Uspješno pohranjeno.");
                await Poruka.ShowAsync();
                

                TextDnevnika = sd.ToString();
                String datum = sd.Datum.Date.ToString("dd.mm.yyyy.");
                DatumText = datum;
                PregledVisibility = Visibility.Visible;
                UnosVisibility = Visibility.Collapsed;
                javnoChecked = false;
                privatnoChecked = false;
                UnosDnevnikaTextBox = "";
                NaslovTextBox = "";
            }
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
            PregledVisibility = Visibility.Collapsed;
            UnosVisibility = Visibility.Visible;
        }
        
        public bool privatnoChecked
        {
            get { return privatno; }
            set
            {
                privatno = value;
                if (value) vidljivost = Vidljivost.Privatno;
                else vidljivost = Vidljivost.Nista;
                NotifyPropertyChanged(nameof(privatnoChecked));
            }
        }

        public bool javnoChecked
        {
            get { return javno; }
            set
            {
                javno = value;
                if (value) vidljivost = Vidljivost.Javno;
                else vidljivost = Vidljivost.Nista;
                NotifyPropertyChanged(nameof(javnoChecked));
            }
        }

    }
}
