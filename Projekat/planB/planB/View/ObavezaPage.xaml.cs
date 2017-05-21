﻿using planB.ViewModel;
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
    public sealed partial class ObavezaPage : Page
    {
        public ObavezaPage()
        {
            this.InitializeComponent();
        }


        private void sider_ValueChanged(object sender, RoutedEventArgs e)
        {
            sliderValue.Text =  slider.Value.ToString();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            DataContext = new ObavezaViewModel();
        }
    }
    
}
