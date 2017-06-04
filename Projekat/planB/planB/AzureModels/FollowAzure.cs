using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.AzureModels
{
    class FollowAzure
    {
        public String id { get; set; }
        public int redniBroj { get; set; }
        public String korisnikID { get; set; }
        public String following_KorisnikID { get; set; }

        public FollowAzure() { }

        public FollowAzure(String _korisnikID, String _following_KorisnikID)
        {
            korisnikID = _korisnikID;
            following_KorisnikID = _following_KorisnikID;
        }
    }
}
