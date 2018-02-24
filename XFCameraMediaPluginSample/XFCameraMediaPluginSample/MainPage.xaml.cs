using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFCameraMediaPluginSample
{
    using Plugin.Geolocator;
    using Plugin.Geolocator.Abstractions;
    using Plugin.Media;

    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();


            this.btnTakePhoto.Clicked += async (sender, args) =>
                {
                    await CrossMedia.Current.Initialize();

                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        DisplayAlert("No Camera", ":( No camera available.", "OK");
                        return;
                    }

                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg"
                    });

                    if (file == null)
                        return;

                    // GPSで位置情報を取得
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50; // <- 1. 50mの精度に指定

                    var position = await locator.GetPositionAsync(timeout: new TimeSpan(0, 0, 0, 100));

                    var msg = $"File Location={file.Path}\n\nAltitude={position.Altitude}\nLongitude={position.Longitude}";
                    await DisplayAlert("Message", msg, "OK");

                    imagePhoto.Source = ImageSource.FromStream(() =>
                        {
                            var stream = file.GetStream();
                            return stream;
                        });
                };
        }
	}
}
