using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.AzureModels
{
    public class KolekcijaAzure
    {
        public String id { get; set; }
        public String naziv { get; set; }
        public String korisnikID { get; set; }
        public DateTime datumKreiranja { get; set; }
        public int redniBroj { get; set; }

        public KolekcijaAzure()
        {
        }

        public KolekcijaAzure(String _id, String _naziv, String _korisnikId, DateTime _datum)
        {
            naziv = _naziv;
            id = _id;
            korisnikID = _korisnikId;
            datumKreiranja = _datum;
        }
    }
}
