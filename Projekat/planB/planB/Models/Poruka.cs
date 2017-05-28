using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.Models
{
    public enum StatusPoruke {Procitano = 1, Neprocitano = 2};
    public class Poruka : INotifyPropertyChanged
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        int id;
        String tekst;
        DateTime datumSlanja;
        int posiljaocID;
        int primaocID;
        StatusPoruke statusPoruke;

        public event PropertyChangedEventHandler PropertyChanged;

        public Poruka() { }
        
        public Poruka(String _tekst, DateTime _datumSlanja)
        {
            tekst = _tekst;
            datumSlanja = _datumSlanja;
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

        public int PosiljaocID
        {
            get { return posiljaocID; }
            set
            {
                posiljaocID = value;
                NotifyPropertyChanged(nameof(PosiljaocID));
            }
        }

        public int PrimaocID
        {
            get { return primaocID; }
            set
            {
                primaocID = value;
                NotifyPropertyChanged(nameof(PrimaocID));
            }
        }

        public String Posiljaoc
        {
            get
            {
                using (var DB = new PlanBDbContext())
                {
                    return DB.Korisnici.Where(x => (x.ID == PosiljaocID)).FirstOrDefault().Ime +
                        DB.Korisnici.Where(x => (x.ID == PosiljaocID)).FirstOrDefault().Prezime;
                }
            }
        }

        public StatusPoruke StatusPoruke
        {
            get { return statusPoruke; }
            set
            {
                statusPoruke = value;
                NotifyPropertyChanged(nameof(StatusPoruke));
            }
        }
    }
}
