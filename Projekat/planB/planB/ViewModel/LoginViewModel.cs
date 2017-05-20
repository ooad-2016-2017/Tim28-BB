using EASendMailRT;
using planB.Models;
using planB.View;
using System;
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

namespace planB.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
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

                Korisnik korisnik = new Korisnik(0, rIme, rPrezime, rKorisnickoIme, rLozinka, rDatumRodjenja, rEmail);
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
                Result = String.Format("Failed to send email with the following error: {0}", ep.Message);
            }

            Poruka = new MessageDialog(Result);
            await Poruka.ShowAsync();
        }
    }
}
