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
    class ObavezaViewModel : INotifyPropertyChanged
    {
        public MessageDialog Poruka { get; set; }
        public String TextObaveze { get; set; }
        public ICommand SpremiButton { get; set; }
        public String sliderVrijednost { get; set; }
        public String DatumText { get; set; }
        public Vidljivost vidljivost;
        private bool privatno;
        private bool javno;
        Korisnik korisnik;

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
                NotifyPropertyChanged(nameof(privatnoChecked));
            }
        }

        public ObavezaViewModel(){
            SpremiButton = new RelayCommand<object>(spremiButton);
            vidljivost = Vidljivost.Nista;
            DatumText = "13.05.2017.";
            TextObaveze = "";
            sliderVrijednost = "0";
            privatnoChecked = false;
            korisnik = LoginViewModel.korisnik;
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
                    DateTime d = DateTime.ParseExact(DatumText, "dd.mm.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
                    //new DateTime(int.Parse(String.Concat(DatumText[6] + DatumText[7] + DatumText[8] + DatumText[9])), int.Parse(String.Concat(DatumText[3] + DatumText[4])), int.Parse(String.Concat(DatumText[0] + DatumText[1])))
                    Obaveza obaveza = new Obaveza(0, new DateTime(1999,1,1), TextObaveze, vidljivost, int.Parse(sliderVrijednost));
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
