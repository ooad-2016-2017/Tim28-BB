using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.Models
{
    public class MuzickaKolekcija : INotifyPropertyChanged
    {
        String naziv;
        List<Pjesma> pjesme;

        public event PropertyChangedEventHandler PropertyChanged;

        public MuzickaKolekcija()
        {
            pjesme = new List<Pjesma>();
        }

        public MuzickaKolekcija(String _naziv, List<Pjesma> _pjesme)
        {
            naziv = _naziv;
            pjesme = _pjesme;
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
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

        public List<Pjesma> Pjesme
        {
            get { return pjesme; }
            set
            {
                pjesme = value;
                NotifyPropertyChanged(nameof(Pjesme));
            }
        }
    }
}
