using System;
using System.IO;
using ChefRisingStar.Services;
using Xamarin.Forms;

namespace ChefRisingStar
{
    public partial class App : Application
    {
        static TempRecipeDatabase database;

        // Create the database connection as a singleton.
        public static TempRecipeDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new TempRecipeDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Recipes.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MockAchievementConditionDataStore>();
            DependencyService.Register<MockAchievementDataStore>();
            DependencyService.Register<SubstitutionCache>();
            DependencyService.Register<IngredientCache>();
            //DependencyService.Register<MockRecipeDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
