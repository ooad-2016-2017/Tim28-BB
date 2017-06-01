using planB.ViewModel;
using planB.View;
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
    public sealed partial class PregledObaveza : Page
    {

        //public Visibility DnevnikVisibility;

        public PregledObaveza()
        {
            this.InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //base.OnNavigatedTo(e);

            DataContext = e.Parameter as PregledObavezaViewModel;

        }

        private void dnevnikButton_Click(object sender, RoutedEventArgs e)
        {
            ProfilPage.frame.Visibility = Visibility.Collapsed;
        }

        private void obavezaButton_Click(object sender, RoutedEventArgs e)
        {
            ProfilPage.frame.Navigate(typeof(ObavezaPage));
        }

    }
}
