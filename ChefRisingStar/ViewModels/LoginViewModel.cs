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
        public Command RegisterCommand { get; protected set; }

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
            RegisterCommand = new Command(OnRegisterClicked);
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

        private async void OnRegisterClicked()
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        }
    }
}
