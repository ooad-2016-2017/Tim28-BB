using planB.Models;
using planB.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace planB.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfilPage : Page
    {
        public static Frame frame;
        ProfilViewModel profilViewModel;

        public ProfilPage()
        {
            this.InitializeComponent();
            frame = myFrame;
            myFrame.Navigate(typeof(PregledObaveza), new PregledObavezaViewModel());
            profilViewModel = new ProfilViewModel();
        }

        private void Odjava(object sender, RoutedEventArgs e)
        {

            ((Frame)Window.Current.Content).Navigate(typeof(LoginPage));
        }

        private void PrikaziDnevnik(object sender, RoutedEventArgs e)
        {
            myFrame.Visibility = Visibility.Visible;
            myFrame.Navigate(typeof(DnevnikPage), new DnevnikViewModel());
        }

        private void PrikaziProfil(object sender, RoutedEventArgs e)
        {
            myFrame.Visibility = Visibility.Visible;
            myFrame.Navigate(typeof(PregledObaveza), new PregledObavezaViewModel());
        }

        private void PrikaziPostavke(object sender, RoutedEventArgs e)
        {
            myFrame.Visibility = Visibility.Visible;
            myFrame.Navigate(typeof(PostavkePage));
        }

        private void PrikaziNovosti(object sender, RoutedEventArgs e)
        {
            myFrame.Visibility = Visibility.Visible;
            myFrame.Navigate(typeof(NovostiPage));
        }

        private void PrikaziMuzickuKolekciju(object sender, RoutedEventArgs e)
        {
            myFrame.Visibility = Visibility.Visible;
            myFrame.Navigate(typeof(MuzickaKolekcijaPage), new MuzickaKolekcijaViewModel());
            //KolekcijaUserControl.Visibility = Visibility.Visible;
            //DnevnikUserControl.Visibility = Visibility.Collapsed;
            //myFrame.Visibility = Visibility.Collapsed;
            //myFrame.Navigate(typeof(MuzickaKolekcijaPage), new MuzickaKolekcijaViewModel());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //base.OnNavigatedTo(e);

            DataContext = profilViewModel;
        }

        private void TraziKorisnike(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            profilViewModel.PretraziKorisnike();
            sender.ItemsSource = profilViewModel.RezultatiPretrage;
        }

        private void PronadjiKorisnika(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            profilViewModel.PronadjiKorisnika();
            sender.ItemsSource = profilViewModel.RezultatiPretrage;
        }

        private void PrikaziProfil(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            Korisnik odabraniKorisnik = args.SelectedItem as Korisnik;
            profilViewModel.PrikaziProfilKorisnika(odabraniKorisnik);
        }
    }
}
