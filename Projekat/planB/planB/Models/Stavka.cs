using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.Models
{
    public enum Vidljivost { Privatno = 1, Javno = 2, Nista = 3}
    public abstract class Stavka : INotifyPropertyChanged
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        int id;
        DateTime datum;
        String sadrzaj;
        Vidljivost vidljivost;
        public String kreatorAzure { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Stavka() { }
        public Stavka(int _id, DateTime _datum, String _sadrzaj, Vidljivost _vidljivost, String kreator)
        {
            datum = _datum;
            sadrzaj = _sadrzaj;
            vidljivost = _vidljivost;
            id = _id;
            kreatorAzure = kreator;
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
            get { return vidljivost; }
            set
            {
                vidljivost = value;
                NotifyPropertyChanged(nameof(Vidljivost));
            }
        }
        
        public String Kreator
        {
            get
            {
                using (var DB = new PlanBDbContext())
                {
                    Korisnik k = DB.Korisnici.Where(x => (x.idAzure == kreatorAzure)).FirstOrDefault();
                    return k.Ime + " " + k.Prezime;
                }
            }

        }

    }
}
