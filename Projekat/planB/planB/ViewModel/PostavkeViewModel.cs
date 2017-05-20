using planB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace planB.ViewModel
{
    class PostavkeViewModel
    {
        public MessageDialog Poruka { get; set; }
        String imeTbx { get; set; }
        String prezimeTbx { get; set; }
        String staraLozinka { get; set; }
        String novaLozinka { get; set; }
        //public ICommand PromjenaDatuma { get; set; }
        public ICommand PotvrdiButton { get; set; }
        DateTime datum;

        Korisnik korisnik { get; set; }

        public PostavkeViewModel()
        {
            korisnik = LoginViewModel.korisnik;
            imeTbx = korisnik.Ime;
            prezimeTbx = korisnik.Prezime;
            staraLozinka = korisnik.Lozinka;
            novaLozinka = "";
            datum = korisnik.DatumRodjenja;
            //PromjenaDatuma = new RelayCommand<object>(promjenaDatuma);
            PotvrdiButton = new RelayCommand<object>(potvrdiButton);
        }
        
        private async void potvrdiButton(object obj)
        {
            if(imeTbx.Length < 2 || prezimeTbx.Length < 3 || novaLozinka.Length < 3)
            {
                Poruka = new MessageDialog("Unesite sve tražene podatke.");
                await Poruka.ShowAsync();
                return;
            }
            using (var DB = new PlanBDbContext())
            {
                LoginViewModel.korisnik.Ime = imeTbx;
                LoginViewModel.korisnik.Prezime = prezimeTbx;
                LoginViewModel.korisnik.Lozinka = novaLozinka;
                LoginViewModel.korisnik.DatumRodjenja = datum;
                DB.SaveChanges();
                Poruka = new MessageDialog("Podaci uspješno izmijenjeni.");
                await Poruka.ShowAsync();
            }

        }
        
    }
}
