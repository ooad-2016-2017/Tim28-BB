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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace planB.ViewModel
{
    class PregledObavezaViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        //public String rPrezime { get; set; }
        public ICommand DnevnikButtonClick { get; set; }
        public Korisnik korisnik;
        public ObservableCollection<Obaveza> _lbxItems { get; set; }
        public ICommand OdabranDatum { get; set; }
        public ICommand ObavezaButtonClick { get; set; }
        DateTime datum;
        public List<Obaveza> ob = new List<Obaveza>();
        int id;



        public PregledObavezaViewModel(int id = -1)
        {
            //jeLiMojProfil = _jeLiMojProfil;
            this.id = id;
            DnevnikButtonClick = new RelayCommand<object>(dnevnikButtonClick);
            ObavezaButtonClick = new RelayCommand<object>(obavezaButtonClick);
            OdabranDatum = new RelayCommand<object>(odabranDatum);
            
            if (id != -1)
            {
                using (var DB = new PlanBDbContext())
                {
                    korisnik = DB.Korisnici.Where(x => (x.ID == id)).FirstOrDefault();
                    //korisnik.Obaveze = DB.Obaveze.Where(x => (x.KreatorID == id)).ToList();
                    foreach (Obaveza o in DB.Obaveze)
                        if (o.KreatorID == id)
                            korisnik.Obaveze.Add(o);
                }
            }
            else
            {
                korisnik = LoginViewModel.korisnik;
            }
           
            lbxItems = new ObservableCollection<Obaveza>();
            datum = DateTime.Now;
            popuniListu();
            /*for (int i=0; i<korisnik.Obaveze.Count(); i++)
                if(korisnik.Obaveze[i].Datum.Date == datum.Date)
                    lbxItems.Add(korisnik.Obaveze[i]);*/
        }

       

        public ObservableCollection<Obaveza> lbxItems
        {
            get { return _lbxItems; }
            set
            {
                _lbxItems = value;
                NotifyPropertyChanged(nameof(lbxItems));
            }
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        

        private void odabranDatum(object obj)
        {
            /*datum = (obj as CalendarView).SelectedDates[0].Date;
            lbxItems.Clear();
            foreach (Obaveza o in korisnik.Obaveze)
                if (o.Datum.Date == datum.Date) lbxItems.Add(o.Sadrzaj);*/

        }

        private void popuniListu()
        {
            foreach (Obaveza o in korisnik.Obaveze)
            {
                if (o.Vidljivost == Vidljivost.Javno)
                {
                    ob.Add(o);
                }
                else
                {
                    if(id == -1)
                    {
                        ob.Add(o);
                    }
                }
            }
            lbxItems = new ObservableCollection<Obaveza>(ob);
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
