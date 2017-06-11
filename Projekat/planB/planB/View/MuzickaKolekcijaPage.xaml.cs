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
    public sealed partial class MuzickaKolekcijaPage : Page
    {
        private MuzickaKolekcijaViewModel muzickaKolekcijaViewModel;
        public MuzickaKolekcijaPage()
        {
            this.InitializeComponent();
            //muzickaKolekcijaViewModel = new MuzickaKolekcijaViewModel();
        }

        private void Search_Artist(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            MuzickaKolekcijaViewModel mkwm = new MuzickaKolekcijaViewModel();
            mkwm.Search_Artist(args.QueryText.ToString());
            sender.ItemsSource = mkwm.rezultatPretrage;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //base.OnNavigatedTo(e);
            DataContext = e.Parameter as MuzickaKolekcijaViewModel;
        }

        private void ArtistIsChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            Pjesma odabranaPjesma = args.SelectedItem as Pjesma;
            MuzickaKolekcijaViewModel mkwm = new MuzickaKolekcijaViewModel();
            mkwm.PrikaziPjesmu(odabranaPjesma);
            //muzickaKolekcijaViewModel.PrikaziPjesmu(odabranaPjesma);
        }
    }
}
