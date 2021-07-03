using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using ChefRisingStar.Models;
using NUnit.Framework;
using Xamarin.Forms.Platform.Android;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Platform = Xamarin.UITest.Platform;

namespace ChefRisingStar.UnitTests
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
            //app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void WelcomeTextIsDisplayed()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("Welcome to Xamarin.Forms!"));
            app.Screenshot("Welcome screen.");

            Assert.IsTrue(results.Any());
        }
        
        [Test]
        public void DeserializeTest()
        {
            try
            {  
                string strResponse = "{\"status\":\"success\",\"ingredient\":\"butter\",\"substitutes\":[\"1 cup = 7 / 8 cup shortening and 1 / 2 tsp salt\",\"1 cup = 7 / 8 cup vegetable oil + 1 / 2 tsp salt\",\"1 / 2 cup = 1 / 4 cup buttermilk +1 / 4 cup unsweetened applesauce\",\"1 cup = 1 cup margarine\"],\"message\":\"Found 4 substitutes for the ingredient.\"}";             
                var substitution = JsonSerializer.Deserialize<Substitution>(strResponse);

                Assert.IsNotNull(substitution);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Could not deserialize Substituion: {ex}");
            }
        }
    }
}
