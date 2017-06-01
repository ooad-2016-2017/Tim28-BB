using planB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.AzureModels
{
    class PorukaAzure
    {
        public String id { get; set; }
        public String tekst { get; set; }
        public DateTime datumSlanja { get; set; }
        public String posiljaocID { get; set; }
        public String primaocID { get; set; }
        public int statusPoruke { get; set; }
        public int redniBroj { get; set; }

        public PorukaAzure() { }

        public PorukaAzure(String _tekst, DateTime _datumSlanja)
        {
            tekst = _tekst;
            datumSlanja = _datumSlanja;
        }

        public StatusPoruke dajStatus()
        {
            if (statusPoruke == 1) return StatusPoruke.Procitano;
            else return StatusPoruke.Neprocitano;
        }

        public void postaviStatus(StatusPoruke status)
        {
            if (status == StatusPoruke.Procitano) statusPoruke = 1;
            else statusPoruke = 2;
        }
    }
}
