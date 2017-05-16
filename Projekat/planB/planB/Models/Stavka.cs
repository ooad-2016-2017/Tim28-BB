using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.Models
{
    public enum Vidljivost { Privatno = 1, Javno = 2}
    public abstract class Stavka : INotifyPropertyChanged
    {
        DateTime datum;
        String sadrzaj;
        Vidljivost vidljivost;

        public event PropertyChangedEventHandler PropertyChanged;

        public Stavka() { }
        public Stavka(DateTime _datum, String _sadrzaj, Vidljivost _vidljivost)
        {
            datum = _datum;
            sadrzaj = _sadrzaj;
            vidljivost = _vidljivost;
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public DateTime Datum
        {
            get { return datum; }
            set
            {
                datum = value;
                NotifyPropertyChanged(nameof(Datum));
            }
        }

        public String Sadrzaj
        {
            get { return sadrzaj; }
            set
            {
                sadrzaj = value;
                NotifyPropertyChanged(nameof(Sadrzaj));
            }
        }

        public Vidljivost Vidljivost
        {
            get { return Vidljivost; }
            set
            {
                vidljivost = value;
                NotifyPropertyChanged(nameof(Vidljivost));
            }
        }
    }
}
