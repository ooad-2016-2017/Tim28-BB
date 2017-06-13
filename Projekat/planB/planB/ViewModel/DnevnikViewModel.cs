using Microsoft.WindowsAzure.MobileServices;
using planB.AzureModels;
using planB.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Popups;

namespace planB.ViewModel
{
    public class DnevnikViewModel : INotifyPropertyChanged
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
        public String NaslovText { get; set; }

        public ObservableCollection<StavkaDnevnika> Dnevnik { get; set; }

        string id;

        public event PropertyChangedEventHandler PropertyChanged;

        public DnevnikViewModel(string id = "")
        {
            this.id = id;
            korisnik = LoginViewModel.korisnik;
            lbxDnevnik = korisnik.Dnevnik;
            DatumText = "";
            TextDnevnika = "";
            NaslovTextBox = "";
            
            stavka = null;
            PregledVisibility = true;
            UnosVisibility = false;
            UnosDnevnikaTextBox = "";
            lbxItems = new List<StavkaDnevnika>();
            lbxDnevnik = new List<StavkaDnevnika>();
            vidljivost = Vidljivost.Nista;

            AddButtonClicked = new RelayCommand<object>(addButtonClicked);
            if (id == "")
            {
                korisnik = LoginViewModel.korisnik;
                DodajDnevnik = new RelayCommand<object>(dodajDnevnikStavku);
            }
            else
            {
                using (var DB = new PlanBDbContext())
                {
                    korisnik = DB.Korisnici.Where(x => (x.idAzure == id)).FirstOrDefault();
                    //korisnik.Obaveze = DB.Obaveze.Where(x => (x.KreatorID == id)).ToList();
                    foreach (StavkaDnevnika sd in DB.Dnevnik)
                        if (sd.kreatorAzure == id)
                            korisnik.Dnevnik.Add(sd);
                }
            }
            Dnevnik = new ObservableCollection<StavkaDnevnika>();
            popuniListu();
            //Dnevnik = new ObservableCollection<StavkaDnevnika>(lbxDnevnik);
            
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
                IMobileServiceTable<StavkaDnevnikAzure> azureObaveze = App.MobileService.GetTable<StavkaDnevnikAzure>();
                List<StavkaDnevnikAzure> listaAzure = await azureObaveze.Where(x => x.id != "").ToListAsync();
                stavka.redniBroj = listaAzure.Count + 1;
                await userTableObj.InsertAsync(stavka);

                StavkaDnevnika sd = new StavkaDnevnika(0, DateTime.Now, UnosDnevnikaTextBox, vidljivost, NaslovTextBox, korisnik.idAzure);
                sd.kreatorAzure = korisnik.idAzure; // M A I D DODAO
                korisnik.Dnevnik.Add(sd);
                DB.Dnevnik.Add(sd);
                DB.SaveChanges();
                lbxItems.Add(sd);
                Poruka = new MessageDialog("Uspješno pohranjeno.");
                await Poruka.ShowAsync();
                

                TextDnevnika = sd.ToString();
                String datum = sd.Datum.Date.ToString("dd.mm.yyyy.");
                DatumText = datum;
                PregledVisibility = true;
                UnosVisibility = false;
                javnoChecked = false;
                privatnoChecked = false;
                UnosDnevnikaTextBox = "";
                NaslovTextBox = "";
            }
        }

        private bool unos;
        public bool UnosVisibility
        {
            get { return unos; }
            set
            {
                unos = value;
                if (unos)
                {
                    PregledVisibility = false;
                }
                NotifyPropertyChanged(nameof(UnosVisibility));
            }
        }

        private bool pregled;
        public bool PregledVisibility
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
                    TextDnevnika = stavka.Sadrzaj;
                    String datum = stavka.Datum.Date.ToString("dd.mm.yyyy.");
                    DatumText = datum;
                    NaslovText = stavka.Naslov;
                    PregledVisibility = true;
                    UnosVisibility = false;
                    NotifyPropertyChanged(nameof(TextDnevnika));
                    NotifyPropertyChanged(nameof(DatumText));
                    NotifyPropertyChanged(nameof(NaslovText));
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
            PregledVisibility = false;
            UnosVisibility = true;
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

        private void popuniListu()
        {
            //String d2 = ((datum.Day <= 9) ? "0" : "") + datum.Day.ToString() + "." + ((datum.Month <= 9) ? "0" : "") + datum.Month.ToString() + "." + datum.Year.ToString() + ".";

            foreach (StavkaDnevnika sd in korisnik.Dnevnik.ToList())
            {
               // String d1 = ((o.Datum.Day <= 9) ? "0" : "") + o.Datum.Day.ToString() + "." + ((o.Datum.Month <= 9) ? "0" : "") + o.Datum.Month.ToString() + "." + o.Datum.Year.ToString() + ".";

                if (sd.Vidljivost == Vidljivost.Javno && !lbxDnevnik.Contains(sd) /*&& d1 == d2*/)
                {
                    lbxDnevnik.Add(sd);
                }
                else
                {
                    if (id == "" && !lbxDnevnik.Contains(sd)/*&& d1 == d2*/)
                    {
                        lbxDnevnik.Add(sd);
                    }
                }
            }
            Dnevnik = new ObservableCollection<StavkaDnevnika>(lbxDnevnik);
        }

    }
}
