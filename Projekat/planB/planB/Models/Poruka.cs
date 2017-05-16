using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.Models
{
    public class Poruka : INotifyPropertyChanged
    {
        String tekst;
        DateTime datumSlanja;
        Korisnik posiljaoc;
        Korisnik primaoc;

        public event PropertyChangedEventHandler PropertyChanged;

        public Poruka() { }
        
        public Poruka(String _tekst, DateTime _datumSlanja, Korisnik _posiljaoc, Korisnik _primaoc)
        {
            tekst = _tekst;
            datumSlanja = _datumSlanja;
            posiljaoc = _posiljaoc;
            primaoc = _primaoc;
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public String Tekst
        {
            get { return tekst; }
            set
            {
                tekst = value;
                NotifyPropertyChanged(nameof(Tekst));
            }
        }

        public DateTime DatumSlanja
        {
            get { return datumSlanja; }
            set
            {
                datumSlanja = value;
                NotifyPropertyChanged(nameof(DatumSlanja));
            }
        }

        public Korisnik Posiljaoc
        {
            get { return posiljaoc; }
            set
            {
                posiljaoc = value;
                NotifyPropertyChanged(nameof(Posiljaoc));
            }
        }

        public Korisnik Primaoc
        {
            get { return primaoc; }
            set
            {
                primaoc = value;
                NotifyPropertyChanged(nameof(Primaoc));
            }
        }
    }
}
