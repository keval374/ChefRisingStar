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
    public class SubstitutionHelperTests
    {
        IApp app;
        Platform platform;

        public SubstitutionHelperTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            //app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void SuccessParseSubstitutions()
        {
            var substitutions = new string[]
            { "1 cup = 7/8 cup shortening and 1/2 tsp salt",
              "1/2 cup = 1/4 cup buttermilk + 1/4 cup unsweetened applesauce",
              "1 cup = 7/8 cup vegetable oil + 1/2 tsp salt",
              "1 cup = 1 cup margarine"};

            var converted = new List<SubstituteIngredient[]>();
            var results = new bool[substitutions.Length];

            for (int i = 0; i < substitutions.Length; i++)
            {
                SubstituteIngredient[] substitutes = SubstitutionHelper.ParseSubstitution(substitutions[i]);
                converted.Add(substitutes);
            }

            foreach (SubstituteIngredient[] sub in converted)
            {
                Assert.IsNotNull(sub);
                Assert.IsNotEmpty(sub);
            }

        }

        [Test]
        public void SuccessEmptySubstitutions()
        {
            var substitutions = new string[]
            { null,
              string.Empty,
              "",
              " ",
              "\t",
              "\n"
            };

            var results = new bool[substitutions.Length];

            for (int i = 0; i < substitutions.Length; i++)
            {
                SubstituteIngredient[] substitutes = SubstitutionHelper.ParseSubstitution(substitutions[i]);
                results[i] = substitutes == null;
            }

            foreach (bool result in results)
            {
                Assert.IsTrue(result);
            }

        }
    }
}
