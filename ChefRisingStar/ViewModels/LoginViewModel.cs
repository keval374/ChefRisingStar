using ChefRisingStar.Views;
using Xamarin.Forms;
using System;

namespace ChefRisingStar.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string email;
        private string password;

        public Action DisplayInvalidLoginPrompt;
        public Command LoginCommand { get; }

        public string Email
        {
            get => email;
            set
            {
                if (email == value)
                    return;

                email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (password == value)
                    return;

                password = value;
                OnPropertyChanged("Password");
            }
            
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked()
        {
            if (email != "keval374@gmail.com" || password != "password")
            {
                DisplayInvalidLoginPrompt();
            }
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(RecipesListPage)}");
        }
    }
}
