using EASendMailRT;
using planB.AzureModels;
using planB.Models;
using planB.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.WindowsAzure.MobileServices;
using planB.DBModels;

namespace planB.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        IMobileServiceTable<KorisnikAzure> userTableObj = App.MobileService.GetTable<KorisnikAzure>();

        public MessageDialog Poruka { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        String VerifikacijskiKod;
      

        // podaci za login korisnika
        public String KorisnickoIme { get; set; }
        public String Lozinka { get; set; }
        public ICommand LoginKorisnika { get; set; }


        // podaci za registraciju korisnika
        public String rIme { get; set; }
        public String rPrezime { get; set; }
        public DateTime rDatumRodjenja { get; set; }
        public DateTimeOffset rDatumRodjenjaOffset { get; set; }
        public String rKorisnickoIme { get; set; }
        public String rLozinka { get; set; }
        public String rLozinkaPotvrda { get; set; }
        public String rEmail { get; set; }
        public String rVerifikacijskiKod { get; set; }
        public ICommand RegistracijaKorisnika { get; set; }
        public ICommand KrajRegistracije { get; set; }
        public static Korisnik korisnik { get; set; }

        private Visibility _podaciVis;
        public Visibility PodaciPanelVisibility
        {
            get { return _podaciVis; }
            set
            {
                _podaciVis = value;
                NotifyPropertyChanged(nameof(PodaciPanelVisibility));
            }
        }

        private Visibility _verifikacijaVis;
        public Visibility VerifikacijaPanelVisiblity
        {
            get { return _verifikacijaVis; }
            set
            {
                _verifikacijaVis = value;
                NotifyPropertyChanged(nameof(VerifikacijaPanelVisiblity));
            }
        }


        public LoginViewModel()
        {
            KorisnickoIme = "";
            Lozinka = "";
            rIme = "";
            rPrezime = "";
            rKorisnickoIme = "";
            rLozinka = "";
            rEmail = "";

            VerifikacijaPanelVisiblity = Visibility.Collapsed;
            PodaciPanelVisibility = Visibility.Collapsed;
            LoginKorisnika = new RelayCommand<object>(loginKorisnika);
            RegistracijaKorisnika = new RelayCommand<object>(registracijaKorisnika);
            KrajRegistracije = new RelayCommand<object>(zavrsiRegistraciju);

            UcitajPodate();
        }

        async void UcitajPodate()
        {
            using (var DB = new PlanBDbContext())
            {

                IMobileServiceTable<KorisnikAzure> azureKorisnik = App.MobileService.GetTable<KorisnikAzure>();

                int broj1 = DB.Korisnici.Count();
                List<KorisnikAzure> listaKorisnika = await azureKorisnik.Where(x => x.id != "").ToListAsync();
                if (broj1 < listaKorisnika.Count)
                {
                    listaKorisnika = await azureKorisnik.Where(x => x.redniBroj > broj1).ToListAsync();
                    
                    foreach (KorisnikAzure o in listaKorisnika)
                    {

                        using (var DB1 = new PlanBDbContext())
                        {
                            Korisnik kk = new Korisnik(0, o.ime, o.prezime, o.korisnickoIme, o.lozinka, o.datumRodjenja, o.email);
                            kk.idAzure = o.id;
                            DB1.Korisnici.Add(kk);
                            DB1.SaveChanges();
                        }
                    }
                }


                IMobileServiceTable<ObavezaAzure> azureObaveze = App.MobileService.GetTable<ObavezaAzure>();

                int broj = DB.Obaveze.Count();
                List<ObavezaAzure> listaAzure = await azureObaveze.Where(x => x.id != "").ToListAsync();
                if (broj < listaAzure.Count)
                {
                    listaAzure = await azureObaveze.Where(x => x.redniBroj > broj).ToListAsync();

                    foreach (ObavezaAzure o in listaAzure)
                    {

                        using (var DB1 = new PlanBDbContext())
                        {
                            Obaveza obaveza = new Obaveza(0, o.datum, o.sadrzaj, o.dajVidljivost(), o.prioritet, o.kreatorID);
                            DB1.Obaveze.Add(obaveza);
                            DB1.SaveChanges();
                        }
                    }
                }

                IMobileServiceTable<StavkaDnevnikAzure> azureDnevnik = App.MobileService.GetTable<StavkaDnevnikAzure>();

                broj = DB.Dnevnik.Count();
                List<StavkaDnevnikAzure> listaDnevnik = await azureDnevnik.Where(x => x.id != "").ToListAsync();
                if (broj < listaAzure.Count)
                {
                    listaAzure = await azureObaveze.Where(x => x.redniBroj > broj).ToListAsync();
                    foreach (StavkaDnevnikAzure o in listaDnevnik)
                    {

                        using (var DB1 = new PlanBDbContext())
                        {
                            StavkaDnevnika sd = new StavkaDnevnika(0, DateTime.Now, o.sadrzaj, o.dajVidljivost(), o.naslov, o.kreatorID);
                            DB1.Dnevnik.Add(sd);
                            DB1.SaveChanges();
                        }
                    }
                }

                IMobileServiceTable<PorukaAzure> porukeAzure = App.MobileService.GetTable<PorukaAzure>();
                broj = DB.Poruke.Count();
                List<PorukaAzure> listaPoruke = await porukeAzure.Where(x => x.redniBroj > 0).ToListAsync();
                if (broj < listaPoruke.Count)
                {
                    listaPoruke = await porukeAzure.Where(x => x.redniBroj > broj).ToListAsync();

                    foreach (PorukaAzure o in listaPoruke)
                    {

                        using (var DB1 = new PlanBDbContext())
                        {
                            Poruka poruka = new Poruka(o.tekst, o.datumSlanja, o.posiljaocID, o.primaocID, o.dajStatus());
                            poruka.idAzure = o.id;
                            DB1.Poruke.Add(poruka);
                            DB1.SaveChanges();
                        }
                    }
                }

                IMobileServiceTable<FollowAzure> followAzure = App.MobileService.GetTable<FollowAzure>();
                broj = DB.Follow.Count();
                List<FollowAzure> listaFollow = await followAzure.Where(x => x.redniBroj > 0).ToListAsync();
                if (broj < listaFollow.Count)
                {
                    listaFollow = await followAzure.Where(x => x.redniBroj > broj).ToListAsync();

                    foreach (FollowAzure o in listaFollow)
                    {

                        using (var DB1 = new PlanBDbContext())
                        {
                            Follow poruka = new Follow();
                            poruka.KorisnikID = o.korisnikID;
                            poruka.Following_KorisnikID = o.following_KorisnikID;
                            DB1.Follow.Add(poruka);
                            DB1.SaveChanges();
                        }
                    }
                }
            }
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private async void loginKorisnika(object parametar)
        {
            using (var DB = new PlanBDbContext())
            {
                korisnik = DB.Korisnici.Where(x => (x.KorisnickoIme == KorisnickoIme && x.Lozinka == Lozinka)).FirstOrDefault();
                if (korisnik == null)
                {
                    Poruka = new MessageDialog("Ne postoji korisnik sa unesenim korisničkim imenom i lozinkom.");
                    await Poruka.ShowAsync();
                   
                }

                else
                {
                    korisnik.Obaveze = new List<Obaveza>();
                    korisnik.Dnevnik = new List<StavkaDnevnika>();

                    foreach (Obaveza o in DB.Obaveze)
                        if (o.kreatorAzure == korisnik.idAzure) korisnik.Obaveze.Add(o);

                    foreach (StavkaDnevnika o in DB.Dnevnik)
                        if (o.kreatorAzure == korisnik.idAzure) korisnik.Dnevnik.Add(o);

                    ((Frame)Window.Current.Content).Navigate(typeof(ProfilPage));
                }

            }
        }

        private async void registracijaKorisnika(object parametar)
        {
            using (var DB = new PlanBDbContext())
            {
                if (rIme.Length < 3 || rPrezime.Length < 3 || rKorisnickoIme.Length < 3 || rLozinka.Length < 3)
                {
                    Poruka = new MessageDialog("Unesite sve tražene podatke.");
                    await Poruka.ShowAsync();
                    return;
                }

                if (!validirajMail())
                {
                    Poruka = new MessageDialog("Neispravna mail adresa.");
                    await Poruka.ShowAsync();
                    return;
                }
                if (rLozinka != rLozinkaPotvrda)
                {
                    Poruka = new MessageDialog("Lozinke se ne podudaraju.");
                    await Poruka.ShowAsync();
                    return;
                }
                rDatumRodjenja = rDatumRodjenjaOffset.Date;
                VerifikacijaPanelVisiblity = Visibility.Visible;
                PodaciPanelVisibility = Visibility.Collapsed;
                await Send_Email(rEmail);

            }
        }

        private async void zavrsiRegistraciju(object parametar)
        {
            using (var DB = new PlanBDbContext())
            {
                if (rVerifikacijskiKod != VerifikacijskiKod)
                {
                    Poruka = new MessageDialog("Uneseni verifikacijski kod nije tačan.");
                    await Poruka.ShowAsync();
                    return;
                }

                KorisnikAzure obj = new KorisnikAzure();
                obj.ime = rIme;
                obj.prezime = rPrezime;
                obj.korisnickoIme = rKorisnickoIme;
                obj.lozinka = rLozinka;
                obj.email = rEmail;
                obj.datumRodjenja = rDatumRodjenja;
                IMobileServiceTable<KorisnikAzure> azureKorisnik = App.MobileService.GetTable<KorisnikAzure>();
                List<KorisnikAzure> listaKorisnika = await azureKorisnik.Where(x => x.id != "").ToListAsync();
                obj.redniBroj = listaKorisnika.Count + 1;
                await userTableObj.InsertAsync(obj);

                Korisnik korisnik = new Korisnik(0, rIme, rPrezime, rKorisnickoIme, rLozinka, rDatumRodjenja, rEmail);
                korisnik.idAzure = obj.id;
                DB.Korisnici.Add(korisnik);
                DB.SaveChanges();
                
                Poruka = new MessageDialog("Uspješno kreiran račun.");
                VerifikacijaPanelVisiblity = Visibility.Collapsed;
                await Poruka.ShowAsync();
            }
        }

        private bool validirajMail()
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(rEmail);
            if (match.Success)
            {
                return true;
            }

            return false;
        }


        private async Task Send_Email(String primaoc)
        {
            String Result = "";
            try
            {
                SmtpMail oMail = new SmtpMail("TryIt");
                SmtpClient oSmtp = new SmtpClient();
                oMail.From = new MailAddress("planbteam387@gmail.com");
                oMail.To.Add(new MailAddress(primaoc));

                Random rnd = new Random();
                VerifikacijskiKod = rnd.Next(10000, 1000000).ToString();
                oMail.Subject = "planB Verifikacija";
                oMail.TextBody = "DOBRODOŠAO/LA U planB!\nVerifikacijski kod: " + VerifikacijskiKod + ".";
                SmtpServer oServer = new SmtpServer("smtp.gmail.com");

                oServer.User = "planbteam387@gmail.com";
                oServer.Password = "vAHosGFF_L024vDt_s3YaRHX";

                oServer.Port = 587;
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                await oSmtp.SendMailAsync(oServer, oMail);
                Result = "Poslan je verifikacijski kod na vaš mail. Za završetak registracije, potrebno je da ga unesete.";
            }
            catch (Exception ep)
            {

                Result = String.Format("Failed to send email with the following error: {0}\nVaš verifikacijski kod: {1}.", ep.Message, VerifikacijskiKod);
            }

            Poruka = new MessageDialog(Result);
            await Poruka.ShowAsync();
        }
    }
}
