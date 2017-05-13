using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.Models
{
    public class StavkaDnevnika
    {
        public DateTime Datum { get; set; }
        public String DatumToString { get; set; }
        public StavkaDnevnika(DateTime datum)
        {
            Datum = datum;
            DatumToString = datum.ToString();
        }
    }
}
