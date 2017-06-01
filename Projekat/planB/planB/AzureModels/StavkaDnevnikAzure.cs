using planB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.AzureModels
{
    //public enum Vidljivost { Privatno = 1, Javno = 2, Nista = 3 }
    public class StavkaDnevnikAzure
    {       
        public String naslov { get; set; }
        public String id { get; set; }
        public DateTime datum { get; set; }
        public String sadrzaj { get; set; }
        public int vidljivost { get; set; }
        public String kreatorID { get; set; }
        public int redniBroj { get; set; }

        public StavkaDnevnikAzure() { }

        public StavkaDnevnikAzure(String _id, DateTime _datum, String _sadrzaj, int _vidljivost, String _naslov, String kreator)
        {
            datum = _datum;
            sadrzaj = _sadrzaj;
            vidljivost = _vidljivost;
            id = _id;
            kreatorID = kreator;
            naslov = _naslov;
        }

        public Vidljivost dajVidljivost()
        {
            if (vidljivost == 1) return Vidljivost.Privatno;
            else if (vidljivost == 2) return Vidljivost.Javno;
            else return Vidljivost.Nista;
        }

        public void postaviVidljivost(Vidljivost vidljiv)
        {
            if (vidljiv == Vidljivost.Privatno) vidljivost = 1;
            else if (vidljiv == Vidljivost.Javno) vidljivost = 2;
            else vidljivost = 3;
        }
    }
}
