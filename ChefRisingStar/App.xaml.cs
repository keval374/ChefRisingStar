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

            DataLoader.LoadData();

            MainPage = new AppShell();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDgyMDA4QDMxMzkyZTMyMmUzMGR6SENOV0phWVo5STZmcmtrY3YrMHcvTER0cmorb1V1T3JXcmRYWkhEUEU9");

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
