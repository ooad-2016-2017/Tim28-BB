﻿using planB.Models;
using planB.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml;

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
        String id;
        public DateTime DatumPicker
        {
            get { return datum; }
            set
            {
                datum = value;
                popuniListu();
                NotifyPropertyChanged(nameof(lbxItems));
            }
        }



        public PregledObavezaViewModel(String id = "")
        {
            //jeLiMojProfil = _jeLiMojProfil;
            this.id = id;
            DnevnikButtonClick = new RelayCommand<object>(dnevnikButtonClick);
            ObavezaButtonClick = new RelayCommand<object>(obavezaButtonClick);
            OdabranDatum = new RelayCommand<object>(odabranDatum);

            if (id != "")
            {
                using (var DB = new PlanBDbContext())
                {
                    korisnik = DB.Korisnici.Where(x => (x.idAzure == id)).FirstOrDefault();
                    //korisnik.Obaveze = DB.Obaveze.Where(x => (x.KreatorID == id)).ToList();
                    foreach (Obaveza o in DB.Obaveze)
                        if (o.kreatorAzure == id)
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
            String d2 = ((datum.Day <= 9) ? "0" : "") + datum.Day.ToString() + "." + ((datum.Month <= 9) ? "0" : "") + datum.Month.ToString() + "." + datum.Year.ToString() + ".";

            foreach (Obaveza o in korisnik.Obaveze)
            {

                String d1 = ((o.Datum.Day <= 9) ? "0" : "") + o.Datum.Day.ToString() + "." + ((o.Datum.Month <= 9) ? "0" : "") + o.Datum.Month.ToString() + "." + o.Datum.Year.ToString() + ".";

                if (o.Vidljivost == Vidljivost.Javno /*&& d1 == d2*/)
                {
                    ob.Add(o);
                }
                else
                {
                    if (id == "" /*&& d1 == d2*/)
                    {
                        ob.Add(o);
                    }
                }
            }
            lbxItems = new ObservableCollection<Obaveza>(ob);
        }

        private void dnevnikButtonClick(object parametar)
        {
            //ProfilPage.frame.Visibility = Visibility.Visible;
            ProfilPage.frame.Navigate(typeof(DnevnikPage));
        }

        private void obavezaButtonClick(object parametar)
        {
            //ProfilPage.frame.Visibility = Visibility.Visible;
            //ProfilPage.frame.Navigate(typeof(ObavezaPage));
            ProfilPage.
                frame.
                Navigate(typeof(ObavezaPage), 
                                              new ObavezaViewModel(DatumPicker));
        }

        /*public event PropertyChangedEventHandler PropertyChanged;
        //public String rPrezime { get; set; }
        public DateTime DatePicker
        {
            get { return datum; }
            set
            {
                datum = value;
                NotifyPropertyChanged(nameof(lbxItems));
            }
        }
        public ICommand DnevnikButtonClick { get; set; }
        public Korisnik korisnik;
        public ObservableCollection<Obaveza> _lbxItems { get; set; }
        public ICommand ObavezaButtonClick { get; set; }
        DateTime datum;
        public List<Obaveza> ob = new List<Obaveza>();
        String id;



        public PregledObavezaViewModel(String id = "")
        {
            //jeLiMojProfil = _jeLiMojProfil;
            this.id = id;
            DnevnikButtonClick = new RelayCommand<object>(dnevnikButtonClick);
            ObavezaButtonClick = new RelayCommand<object>(obavezaButtonClick);
            
            if (id != "")
            {
                using (var DB = new PlanBDbContext())
                {
                    korisnik = DB.Korisnici.Where(x => (x.idAzure == id)).FirstOrDefault();
                    //korisnik.Obaveze = DB.Obaveze.Where(x => (x.KreatorID == id)).ToList();
                    foreach (Obaveza o in DB.Obaveze)
                        if (o.kreatorAzure == id)
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
        /*}



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

            private void popuniListu()
            {
                foreach (Obaveza o in korisnik.Obaveze)
                {
                    if (o.Vidljivost == Vidljivost.Javno && o.Datum == datum)
                    {
                        ob.Add(o);
                    }
                    else
                    {
                        if(id == "" && o.Datum == datum)
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
                ProfilPage.
                    frame.
                    Navigate(typeof(ObavezaPage));//, 
                    //new ObavezaViewModel(DateTime.Now));
            }*/
    }
}
