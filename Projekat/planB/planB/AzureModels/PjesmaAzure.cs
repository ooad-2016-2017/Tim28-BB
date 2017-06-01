using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.AzureModels
{
    public class PjesmaAzure
    {
        public String id { get; set; }
        public String izvodjac { get; set; }
        public String naziv { get; set; }
        public String preview { get; set; }
        public String urlSlike { get; set; }
        public String muzickaKolekcijaID { get; set; }
        public int redniBroj { get; set; }

        public PjesmaAzure() { }

        public PjesmaAzure(String _izvodjac, String _naziv, String _preview, String _urlSlike)
        {
            izvodjac = _izvodjac;
            naziv = _naziv;
            preview = _preview;
            urlSlike = _urlSlike;
        }
    }
}
