using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFCameraMediaPluginSample
{
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

		            await DisplayAlert("File Location", file.Path, "OK");

		            imagePhoto.Source = ImageSource.FromStream(() =>
		                {
		                    var stream = file.GetStream();
		                    return stream;
		                });
		        };
		}
	}
}
