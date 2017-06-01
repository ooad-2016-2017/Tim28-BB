using planB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.AzureModels
{
    public class ObavezaAzure
    {
        public int prioritet { get; set; }
        public String id { get; set; }
        public DateTime datum { get; set; }
        public String sadrzaj { get; set; }
        public int vidljivost { get; set; }
        public String kreatorID { get; set; }
        public int redniBroj { get; set; }

        public ObavezaAzure() { }

        public ObavezaAzure(String _id, DateTime _datum, String _sadrzaj, int _vidljivost, int _prioritet, String kreator)
        {
            prioritet = _prioritet;
            datum = _datum;
            sadrzaj = _sadrzaj;
            vidljivost = _vidljivost;
            id = _id;
            kreatorID = kreator;
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
