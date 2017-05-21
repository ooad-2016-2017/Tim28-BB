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

        public ProfilPage()
        {
            this.InitializeComponent();
            frame = myFrame;
            myFrame.Navigate(typeof(PregledObaveza));
        }

        private void PrikaziDnevnik(object sender, RoutedEventArgs e)
        {
            myFrame.Visibility = Visibility.Visible;
            myFrame.Navigate(typeof(DnevnikPage));
        }

        private void PrikaziProfil(object sender, RoutedEventArgs e)
        {
            myFrame.Visibility = Visibility.Visible;
            myFrame.Navigate(typeof(PregledObaveza));
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
        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //base.OnNavigatedTo(e);

            DataContext = new ProfilViewModel();
        }
    }
}
