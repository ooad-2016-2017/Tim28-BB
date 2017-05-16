using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.Models
{
    public class Pjesma : INotifyPropertyChanged
    {
        String izvodjac;
        String naziv;

        public event PropertyChangedEventHandler PropertyChanged;

        public Pjesma() { }
        
        public Pjesma (String _izvodjac, String _naziv)
        {
            izvodjac = _izvodjac;
            naziv = _naziv;
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public String Izvodjac
        {
            get { return izvodjac; }
            set
            {
                izvodjac = value;
                NotifyPropertyChanged(nameof(Izvodjac));
            }
        }

        public String Naziv
        {
            get { return naziv; }
            set
            {
                naziv = value;
                NotifyPropertyChanged(nameof(Naziv));
            }
        }
    }
}
