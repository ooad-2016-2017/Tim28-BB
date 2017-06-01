using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using planB.AzureModels;

namespace planB.Models
{
    public class Obaveza : Stavka, INotifyPropertyChanged
    {
        int prioritet;

        public event PropertyChangedEventHandler _PropertyChanged;

        public Obaveza() : base() { }

        public Obaveza(int _id, DateTime _datum, String _sadrzaj, Vidljivost _vidljivost, int _prioritet, String kreator) :
            base(_id, _datum, _sadrzaj, _vidljivost, kreator)
        {
            prioritet = _prioritet;
        }
        
        public override string ToString()
        {
            return Sadrzaj;
        }

        private void NotifyPropertyChanged(String info)
        {
            if (_PropertyChanged != null)
            {
                _PropertyChanged(this, new PropertyChangedEventArgs(info));
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
