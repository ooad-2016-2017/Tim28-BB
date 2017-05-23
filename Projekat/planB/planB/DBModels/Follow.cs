using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planB.DBModels
{
    public class Follow
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        int id;
        int korisnikID;
        int following_KorisnikID;

        public Follow() { }

        public Follow(int _id, int _korisnikID, int _following_KorisnikID)
        {
            id = _id;
            korisnikID = _korisnikID;
            following_KorisnikID = _following_KorisnikID;
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int KorisnikID
        {
            get { return korisnikID; }
            set { korisnikID = value; }
        }

        public int Following_KorisnikID
        {
            get { return following_KorisnikID; }
            set { following_KorisnikID = value; }
        }
    }
}
