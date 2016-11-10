using Acr.UserDialogs;
using Plugin.LocalNotifications;
using Plugin.Media;
using Plugin.Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace App1
{
    public class ImageList
    {
        public string ImageUrl { get; set; }
    }
    public partial class Page1 : ContentPage
    {
        
        private string url = "http://xamarinsample.xamarintr.com/Userimage/";
        private readonly IList<ImageList> model = new ObservableCollection<ImageList>();
        public Page1()
        {
            BindingContext = model;
            InitializeComponent();
        }

        private async void onTakePhoto(object sender, EventArgs e)
        {
            App.ImageName = Guid.NewGuid().ToString() + ".jpg";
            if (!CrossMedia.Current.IsCameraAvailable ||
               !CrossMedia.Current.IsTakePhotoSupported)
            {
                DisplayAlert("Hata", "Kamera Kullanılamıyor!", "Tamam");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = App.ImageName,
                SaveToAlbum = true,
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front
            });

            if (file == null)
            {
                return;
            }

            var stream = file.GetStream();

            UserDialogs.Instance.ShowLoading("Loading..");
            ServiceManager manager = new ServiceManager();
            ApiResult result = await manager.upload(stream);
            stream.ReadByte();

            if (result.Result)
            {
                UserDialogs.Instance.HideLoading();
                CrossLocalNotifications.Current.Show("Tebrikler!",
                     "Fotoğraf yüklemeniz başarılı bir şekilde gerçekleşti");
                model.Add(new ImageList
                {
                    ImageUrl = url + App.ImageName
                });
                bool shareResult = await DisplayAlert("Share", "Do you want to share your photo?", "Yes", "No");
                if (shareResult)
                {
                    await CrossShare.Current.ShareLink(url + App.ImageName, "Xamarin Dev Days", "Share!");
                }
            }
            else
            {
                UserDialogs.Instance.ShowError(result.Message);
            }

            image.Source = ImageSource.FromStream(() =>
            {
                file.Dispose();
                return stream;
            });
        }

        private void OnOpen(object sender, EventArgs e)
        {

        }
    }
}