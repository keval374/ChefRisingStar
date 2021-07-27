using ChefRisingStar.Services;
using Xamarin.Forms;

namespace ChefRisingStar
{
    public partial class App : Application
    {

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
