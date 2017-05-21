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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace planB.View
{
    public sealed partial class KolekcijaKorisnickaKontrola : UserControl
    {
        private MuzickaKolekcijaViewModel muzickaKolekcijaViewModel;
        public KolekcijaKorisnickaKontrola()
        {
            this.InitializeComponent();
            muzickaKolekcijaViewModel = new MuzickaKolekcijaViewModel();
            DataContext = this;
        }

        private void Search_Artist(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            
        }

        
    }
}       
