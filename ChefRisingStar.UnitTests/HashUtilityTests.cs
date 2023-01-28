using ChefRisingStar.Helpers;
using ChefRisingStar.Models;
using NUnit.Framework;
using System.Collections.Generic;
using Xamarin.UITest;
using Platform = Xamarin.UITest.Platform;

namespace ChefRisingStar.UnitTests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class HashUtilityTests
    {
        IApp app;
        Platform platform;

        public HashUtilityTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            //app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void SuccessHashPassword()
        {
            
        }
    }
}
