using ChefRisingStar.Helpers;
using ChefRisingStar.Services;
using ChefRisingStar.Views;
using System;
using System.IO;
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
            DependencyService.Register<RestHelper>();
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MockAchievementStepDataStore>();
            DependencyService.Register<MockAchievementDataStore>();
            DependencyService.Register<UserDataStore>();
            DependencyService.Register<MockSchooDataStore>();
            DependencyService.Register<MockTeamDataStore>();
            DependencyService.Register<MockLanguageDataStore>();
            DependencyService.Register<MockFavoritesDataStore>();

            DependencyService.Register<SubstitutionCache>();
            DependencyService.Register<IngredientCache>();
            //DependencyService.Register<MockRecipeDataStore>();

            DataLoader.LoadData();

            RestHelper helper = DependencyService.Get<RestHelper>();
            helper.AuthenticationRequired += OnAuthenticationRequired;

            MainPage = new AppShell();
        }

        private void OnAuthenticationRequired(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
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
