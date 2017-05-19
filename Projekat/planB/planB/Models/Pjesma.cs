using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.Models
{
    public class Pjesma : INotifyPropertyChanged
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        int id;
        String izvodjac;
        String naziv;

        public event PropertyChangedEventHandler PropertyChanged;

        public Pjesma() { }
        
        public Pjesma (int _id, String _izvodjac, String _naziv)
        {
            izvodjac = _izvodjac;
            naziv = _naziv;
            id = _id;
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        
        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged(nameof(ID));
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
