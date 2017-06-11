using Microsoft.WindowsAzure.MobileServices;
using planB.AzureModels;
using planB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace planB.ViewModel
{
    public class ObavezaViewModel : INotifyPropertyChanged
    {

        IMobileServiceTable<ObavezaAzure> userTableObj = App.MobileService.GetTable<ObavezaAzure>();

        public MessageDialog Poruka { get; set; }
        public String TextObaveze { get; set; }
        public ICommand SpremiButton { get; set; }
        public String sliderVrijednost { get; set; }
        public String DatumText { get; set; }
        public Vidljivost vidljivost;
        private bool privatno;
        private bool javno;
        Korisnik korisnik;
        DateTime datum;

        public event PropertyChangedEventHandler PropertyChanged;

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

        public ObavezaViewModel(DateTime date = new DateTime()){
            SpremiButton = new RelayCommand<object>(spremiButton);
            vidljivost = Vidljivost.Nista;
            DatumText = //date.ToString("dd.mm.yyyy.");
               ((date.Day <= 9)? "0" : "") + date.Day.ToString() + "." + ((date.Month <= 9) ? "0" : "") + date.Month.ToString() + "." +
                date.Year.ToString() + ".";
            TextObaveze = "";
            sliderVrijednost = "0";
            privatnoChecked = false;
            korisnik = LoginViewModel.korisnik;
            datum = date;
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        

        private async void spremiButton(object obj)
        {
            using (var DB = new PlanBDbContext())
            {
                if (TextObaveze.Length < 3 || vidljivost == Vidljivost.Nista)
                {
                    Poruka = new MessageDialog("Unesite sve tražene podatke.");
                    await Poruka.ShowAsync();
                    return;
                }

                else
                {
                    // DateTime d = DateTime.ParseExact(DatumText, "dd.mm.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
                    //new DateTime(int.Parse(String.Concat(DatumText[6] + DatumText[7] + DatumText[8] + DatumText[9])), int.Parse(String.Concat(DatumText[3] + DatumText[4])), int.Parse(String.Concat(DatumText[0] + DatumText[1])))

                    ObavezaAzure obavezaAzure = new ObavezaAzure();
                    obavezaAzure.datum = datum;
                    obavezaAzure.kreatorID = korisnik.idAzure;
                    obavezaAzure.postaviVidljivost(vidljivost);
                    obavezaAzure.sadrzaj = TextObaveze;
                    obavezaAzure.prioritet = int.Parse(sliderVrijednost);
                    IMobileServiceTable<ObavezaAzure> azureObaveze = App.MobileService.GetTable<ObavezaAzure>();
                    List<ObavezaAzure> listaAzure = await azureObaveze.Where(x => x.id != "").ToListAsync();
                    obavezaAzure.redniBroj = listaAzure.Count + 1;
                    await userTableObj.InsertAsync(obavezaAzure);

                    Obaveza obaveza = new Obaveza(0, datum, TextObaveze, vidljivost, int.Parse(sliderVrijednost), korisnik.idAzure);
                    obaveza.kreatorAzure = korisnik.idAzure; // M A I D DODAO
                    korisnik.Obaveze.Add(obaveza);
                    DB.Obaveze.Add(obaveza);
                    DB.SaveChanges();
                    Poruka = new MessageDialog("Uspješno pohranjena obaveza.");
                    await Poruka.ShowAsync();
                }
            }
        }
    }
}
