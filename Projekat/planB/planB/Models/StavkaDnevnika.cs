using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.Models
{
    public class StavkaDnevnika : Stavka, INotifyPropertyChanged
    {
        String naslov;

        public event PropertyChangedEventHandler _PropertyChanged;

        public StavkaDnevnika() : base() { }

        public StavkaDnevnika(int _id, DateTime _datum, String _sadrzaj, Vidljivost _vidljivost, String _naslov) :
            base(_id, _datum, _sadrzaj, _vidljivost)
        {
            naslov = _naslov;
        }


        private void NotifyPropertyChanged(String info)
        {
            if (_PropertyChanged != null)
            {
                _PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }


        public String Naslov
        {
            get { return naslov; }
            set
            {
                naslov = value;
                NotifyPropertyChanged(nameof(Naslov));
            }
        }
    }
}
