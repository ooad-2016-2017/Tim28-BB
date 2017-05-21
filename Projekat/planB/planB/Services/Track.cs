using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.Services
{
    public class Track
    {
        public String Name { get; set; }
        public String Preview { get; set; }
        public String PhotoUrl { get; set; }

        public Track()
        {
            Name = "";
            Preview = "";
            PhotoUrl = "";
        }
    }
}
