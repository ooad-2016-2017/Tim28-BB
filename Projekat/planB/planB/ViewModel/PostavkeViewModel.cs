using planB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace planB.ViewModel
{
    class PostavkeViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //kontrola krsenje mvvm
        CaptureElement PreviewControl { get; set; }
        public MessageDialog Poruka { get; set; }
        public String ime;
        public ICommand Uslikaj { get; set; }
        public ICommand Promijeni { get; set; }
        public ICommand PotvrdiButton { get; set; }
        DateTime datum;
        public String prezime;
        public String stara;
        public String nova;
        Image slika;
        public Image ImageControl
        {
            get { return slika; }
            set
            {
                slika = value;
                NotifyPropertyChanged(nameof(ImageControl));
            }
        }

        Korisnik korisnik { get; set; }

        /*public CameraHelper Camera { get; set; }
        //Negdje privremeno mora biti slika koja ce se prikazati kad se uslika*/
        private BitmapImage SlikaControl;
        public BitmapImage Slika
        {
            get { return SlikaControl; }
            set
            {
                SlikaControl = value;
                OnNotifyPropertyChanged("Slika");
            }
        }

        public String imeTbx
        {
            get { return ime; }
            set
            {
                if (value != ime)
                {
                    ime = value;
                    NotifyPropertyChanged(nameof(imeTbx));
                }
            }
        }

        public String prezimeTbx
        {
            get { return prezime; }
            set
            {
                if (value != prezime)
                {
                    prezime = value;
                    NotifyPropertyChanged(nameof(prezimeTbx));
                }
            }
        }
        public String staraLozinka
        {
            get { return stara; }
            set
            {
                if (value != stara)
                {
                    stara = value;
                    NotifyPropertyChanged(nameof(staraLozinka));
                }
            }
        }
        public String novaLozinka
        {
            get { return nova; }
            set
            {
                if (value != nova)
                {
                    nova = value;
                    NotifyPropertyChanged(nameof(novaLozinka));
                }
            }
        }


        public PostavkeViewModel()
        {
            korisnik = LoginViewModel.korisnik;
            imeTbx = korisnik.Ime;
            prezimeTbx = korisnik.Prezime;
            staraLozinka = "";
            novaLozinka = "";
            datum = korisnik.DatumRodjenja;
            //PromjenaDatuma = new RelayCommand<object>(promjenaDatuma);
            PotvrdiButton = new RelayCommand<object>(potvrdiButton);

            /*Camera = new CameraHelper(PreviewControl);
            Camera.InitializeCameraAsync();*/
            Uslikaj = new RelayCommand<object>(uslikaj);            Promijeni = new RelayCommand<object>(promijeni);
            SlikaControl = null;
        }

        /*public async void uslikaj(object parametar)
        {
            await Camera.TakePhotoAsync(SlikanjeGotovo);
        }

        public void SlikanjeGotovo(SoftwareBitmapSource slikica)
        {
            Slika = slikica;
            Slika = Camera.SlikaBitmap;
        }*/


        protected void OnNotifyPropertyChanged([CallerMemberName] string memberName = "")
        {
            //? je skracena verzija ako nije null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }

        private async void potvrdiButton(object obj)
        {
            if (imeTbx.Length < 2 || prezimeTbx.Length < 3)
            {
                Poruka = new MessageDialog("Unesite sve tražene podatke.");
                await Poruka.ShowAsync();
                return;
            }
            if ((staraLozinka.Length != 0 && staraLozinka != korisnik.Lozinka) || (novaLozinka.Length != 0 && staraLozinka != korisnik.Lozinka))
            {
                Poruka = new MessageDialog("Lozinka nije ispravna.");
                await Poruka.ShowAsync();
                return;
            }

            using (var DB = new PlanBDbContext())
            {
                korisnik.Ime = imeTbx;
                korisnik.Prezime = prezimeTbx;
                korisnik.Lozinka = novaLozinka;
                korisnik.DatumRodjenja = datum;
                DB.Korisnici.Update(korisnik);
                DB.SaveChanges();// DB.Korisnici.Where(x => (x.KorisnickoIme == korisnik.KorisnickoIme)).FirstOrDefault());
                Poruka = new MessageDialog("Podaci uspješno izmijenjeni.");
                await Poruka.ShowAsync();
            }

        }



        private void NotifyPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        private StorageFile storeFile;
        private IRandomAccessStream stream;

        private async void uslikaj(object sender)
        {
            CameraCaptureUI capture = new CameraCaptureUI();
            capture.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            capture.PhotoSettings.CroppedAspectRatio = new Size(3, 5);
            capture.PhotoSettings.MaxResolution = CameraCaptureUIMaxPhotoResolution.HighestAvailable;
            storeFile = await capture.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (storeFile != null)
            {
                BitmapImage bimage = new BitmapImage();
                stream = await storeFile.OpenAsync(FileAccessMode.Read); ;
                bimage.SetSource(stream);
                Slika = bimage;
                save(sender);

            }
        }

        private async void promijeni(object sender)
        {
            var picker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                FileTypeFilter = { ".jpg", ".jpeg", ".png", ".gif" }
            };
            var file = await picker.PickSingleFileAsync();
        }
        
        private async void save(object sender)
        {
            try
            {

                FileSavePicker fs = new FileSavePicker();
                fs.FileTypeChoices.Add("Image", new List<string>() { ".jpeg" });
                fs.DefaultFileExtension = ".jpeg";
                fs.SuggestedFileName = "Image" + DateTime.Today.ToString();
                fs.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                fs.SuggestedSaveFile = storeFile;
                // Saving the file 
                var s = await fs.PickSaveFileAsync();
                if (s != null)
                {
                    using (var dataReader = new DataReader(stream.GetInputStreamAt(0)))
                    {
                        await dataReader.LoadAsync((uint)stream.Size);
                        byte[] buffer = new byte[(int)stream.Size];
                        dataReader.ReadBytes(buffer);
                        await FileIO.WriteBytesAsync(s, buffer);
                    }
                }
            }
            catch (Exception ex)
            {
                var messageDialog = new MessageDialog("Unable to save now.");
                await messageDialog.ShowAsync();
            }

        }
    }
}