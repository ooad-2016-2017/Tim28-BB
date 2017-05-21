using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.Services
{
    public class SearchingResult
    {
        public String Artist { get; set; }
        public List<Track> Tracks { get; set; }

        public SearchingResult()
        {
            Artist = "";
            Tracks = new List<Track>();
        }
    }
}
