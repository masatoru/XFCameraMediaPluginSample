using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Media;
using Prism.Services;
using Xamarin.Forms;

namespace XFCameraMediaPluginSample.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private INavigationService NavigationService;

        private IPageDialogService PageDialogService { get; }

        private ImageSource imageSource;

        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { this.SetProperty(ref this.imageSource, value); }
        }

        public DelegateCommand TakePhotoCommand { get; }

        private bool isBusy;

        public bool IsBusy
        {
            get { return this.isBusy; }
            set { this.SetProperty(ref this.isBusy, value); }
        }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            NavigationService = navigationService;
            this.PageDialogService = pageDialogService;
            this.TakePhotoCommand = new DelegateCommand(async () => await this.TakePhotoAsync());
        }

        private async Task TakePhotoAsync()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await PageDialogService.DisplayAlertAsync("No Camera", ":( No camera available.", "OK");
                return;
            }

            try
            {
                IsBusy = true;
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
                await PageDialogService.DisplayAlertAsync("Message", msg, "OK");

                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
            finally
            {
                IsBusy = false;
            }
        }


        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public async void OnNavigatingTo(NavigationParameters parameters)
        {
            // 最初にカメラを起動する
            await TakePhotoAsync();
        }
    }
}
