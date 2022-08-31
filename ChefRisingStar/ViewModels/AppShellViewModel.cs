using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChefRisingStar.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        public AppShellViewModel()
        {
            CurrentUser = new Models.User
            {
                UserName = "Fallout",
                EmailAddress = "fallout99@hotmail.com",
                IsAdmin = true
            };
        }
    }
}