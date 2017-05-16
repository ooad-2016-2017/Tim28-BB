using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.Models
{
    public class Obaveza : Stavka, INotifyPropertyChanged
    {
        int prioritet;

        public event PropertyChangedEventHandler PropertyChanged;

        public Obaveza() : base() { }

        public Obaveza(DateTime _datum, String _sadrzaj, Vidljivost _vidljivost, int _prioritet) :
            base(_datum, _sadrzaj, _vidljivost)
        {
            prioritet = _prioritet;
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public int Prioritet
        {
            get { return prioritet; }
            set
            {
                prioritet = value;
                NotifyPropertyChanged(nameof(Prioritet));
            }
        }
    }
}
