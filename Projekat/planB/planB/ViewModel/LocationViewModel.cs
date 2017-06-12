using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml.Controls.Maps;

namespace planB.ViewModel
{
    class LocationViewModel : INotifyPropertyChanged
    {
        private Geopoint trenutnaLokacija;
        public Geopoint TrenutnaLokacija
        {
            get { return trenutnaLokacija; }
            set
            {
                trenutnaLokacija = value;
                NotifyPropertyChanged("TrenutnaLokacija");
            }
        }
        private string lokacija;
        public string Lokacija
        {
            get { return lokacija; }
            set
            {
                lokacija = value;
                NotifyPropertyChanged("Lokacija");
            }
        }
        private string adresa;
        public string Adresa
        {
            get { return adresa; }
            set
            {
                adresa = value;
                NotifyPropertyChanged("Adresa");
            }
        }

        private Boolean adresaVisibility;
        public Boolean AdresaVisibility
        {
            get { return adresaVisibility; }
            set
            {
                adresaVisibility = value;
                NotifyPropertyChanged(nameof(AdresaVisibility));
            }
        }

        private Boolean mapaVisibility;
        public Boolean MapaVisibility
        {
            get { return mapaVisibility; }
            set
            {
                mapaVisibility = value;
                NotifyPropertyChanged(nameof(MapaVisibility));
            }
        }

        private Boolean shareVisibility;
        public Boolean ShareVisibility
        {
            get { return shareVisibility; }
            set
            {
                shareVisibility = value;
                NotifyPropertyChanged(nameof(ShareVisibility));
            }
        }

        public ICommand LocirajKorisnika { get; set; }
      
        MapControl Mapa;
        public LocationViewModel(MapControl mapa)
        {
            Mapa = mapa;
            AdresaVisibility = false;
            MapaVisibility = false;
            ShareVisibility = false;
            LocirajKorisnika = new RelayCommand<object>(locirajKorisnika);
            //dajLokaciju();
        }

        public async void locirajKorisnika(object parametar)
        {
            MapaVisibility = true;
            AdresaVisibility = true;
            //ShareVisibility = true;
            Geoposition pos = null;
            
            var accessStatus = await Geolocator.RequestAccessAsync();
            if (accessStatus == GeolocationAccessStatus.Allowed)
            {
                Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = 10 };
                pos = await geolocator.GetGeopositionAsync();
            }
            TrenutnaLokacija = pos.Coordinate.Point;
            Lokacija = "Geolokacija Lat: " + TrenutnaLokacija.Position.Latitude + " Lng: " +
            TrenutnaLokacija.Position.Longitude;
            MapLocationFinderResult result = await
            MapLocationFinder.FindLocationsAtAsync(pos.Coordinate.Point);
            if (result.Status == MapLocationFinderStatus.Success)
            {
                Adresa = "Adresa: " + result.Locations[0].Address.Street;
            }
            
            double centerLatitude = Mapa.Center.Position.Latitude;
            double centerLongitude = Mapa.Center.Position.Longitude;
            MapPolyline mapPolyline = new MapPolyline();
            mapPolyline.Path = new Geopath(new List<BasicGeoposition>() {
                new BasicGeoposition() {Latitude=centerLatitude-0.0005, Longitude=centerLongitude-0.001 },
                new BasicGeoposition() {Latitude=centerLatitude+0.0005, Longitude=centerLongitude-0.001 },
                new BasicGeoposition() {Latitude=centerLatitude+0.0005, Longitude=centerLongitude+0.001 },
                new BasicGeoposition() {Latitude=centerLatitude-0.0005, Longitude=centerLongitude+0.001 },
                new BasicGeoposition() {Latitude=centerLatitude-0.0005, Longitude=centerLongitude-0.001 }
 });
            mapPolyline.StrokeColor = Colors.Black;
            mapPolyline.StrokeThickness = 3;
            mapPolyline.StrokeDashed = true;
            Mapa.MapElements.Add(mapPolyline);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
