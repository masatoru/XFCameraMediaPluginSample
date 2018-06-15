using Prism;
using Prism.Ioc;
using XFCameraMediaPluginSample.ViewModels;
using XFCameraMediaPluginSample.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Microsoft.AppCenter.Push;
using XFCameraMediaPluginSample.Helpers;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XFCameraMediaPluginSample
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
            // Handle when your app starts
            Microsoft.AppCenter.AppCenter.Start(AppConstants.AppCenterStart,
                               typeof(Analytics), typeof(Crashes), typeof(Distribute), typeof(Push));
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
        }
    }
}
