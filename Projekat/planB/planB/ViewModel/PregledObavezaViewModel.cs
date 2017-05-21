using planB.Models;
using planB.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace planB.ViewModel
{
    class PregledObavezaViewModel
    {

        //public String rPrezime { get; set; }
        public ICommand DnevnikButtonClick { get; set; }
        public Korisnik korisnik;
        public List<Obaveza> lbxItems { get; set; }
        public ICommand OdabranDatum { get; set; }
        public ICommand ObavezaButtonClick { get; set; }
        DateTime datum;



        public PregledObavezaViewModel()
        {
            DnevnikButtonClick = new RelayCommand<object>(dnevnikButtonClick);
            ObavezaButtonClick = new RelayCommand<object>(obavezaButtonClick);
            OdabranDatum = new RelayCommand<object>(odabranDatum);
            korisnik = LoginViewModel.korisnik;
            lbxItems = new List<Obaveza>();
            datum = DateTime.Now;
            for (int i=0; i<korisnik.Obaveze.Count(); i++)
                if(korisnik.Obaveze[i].Datum.Date == datum.Date)
                    lbxItems.Add(korisnik.Obaveze[i]);
        }

        private void odabranDatum(object obj)
        {
            /*datum = (obj as CalendarView).SelectedDates[0].Date;
            lbxItems.Clear();
            foreach (Obaveza o in korisnik.Obaveze)
                if (o.Datum.Date == datum.Date) lbxItems.Add(o.Sadrzaj);*/

        }
        
        private void dnevnikButtonClick(object parametar)
        {
            ProfilPage.frame.Visibility = Visibility.Visible;
            ProfilPage.frame.Navigate(typeof(DnevnikPage));
        }

        private void obavezaButtonClick(object parametar)
        {
            ProfilPage.frame.Visibility = Visibility.Visible;
            ProfilPage.frame.Navigate(typeof(ObavezaPage));
        }
    }
}
