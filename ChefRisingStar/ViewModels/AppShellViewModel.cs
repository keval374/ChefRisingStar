namespace ChefRisingStar.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        public AppShellViewModel()
        {
            CurrentUser = new Models.User
            {
                Id = 1,
                UserName = "Fallout",
                Email = "fallout99@hotmail.com",
                IsAdministrator = true
            };
        }
    }
}