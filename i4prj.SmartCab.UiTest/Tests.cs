using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace i4prj.SmartCab.UiTest
{
    [TestFixture(Platform.Android)]
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
        }

        [Test]
        public void Test()
        {
            app.Tap("GoToCreateCustomer");

            app.Screenshot("firstthingsfirst");
        }
    }
}
