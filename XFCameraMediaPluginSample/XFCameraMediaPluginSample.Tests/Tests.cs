using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace XFCameraMediaPluginSample.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
            app.WaitForElement("MainPage", "Timeout", TimeSpan.FromSeconds(10));
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        public async Task TakePhotoTest()
        {
            app.Tap("TakePhotoCommand");
            await Task.Delay(250);
            // TODO 期待される結果を書くこと。
        }

        [Test]
        public async Task GetLocationTest()
        {
            app.Tap("GetLocationCommand");
            await Task.Delay(250);
        }
    }
}
