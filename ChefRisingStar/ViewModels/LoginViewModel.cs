using ChefRisingStar.Helpers;
using ChefRisingStar.Views;
using ChefRisingStar.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using Xamarin.Forms;

namespace ChefRisingStar.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string username;
        private string password;

        public Action DisplayInvalidLoginPrompt;
        public Command LoginCommand { get; }
        public Command RegisterCommand { get; protected set; }

        public string Username
        {
            get => username;
            set
            {
                if (username == value)
                    return;

                username = value;
                OnPropertyChanged("Username");
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
            try
            {
                IsBusy = true;

                RestHelper helper = DependencyService.Get<RestHelper>();
                var userCred = new UserCred(Username, Password);
                string token = await helper.Post<UserCred, string>(userCred, "api/auth/authenticate");

                if (token == null)
                {
                    DisplayInvalidLoginPrompt();
                    return;
                }

                helper.SetBearer(token);
                MetricHelper.SendMetric(new AppMetric(MetricType.UserLoggedIn, 1, username));

                await Shell.Current.GoToAsync($"//{nameof(RecipesListPage)}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during login: {ex}");
                await Application.Current.MainPage.DisplayAlert("API Error:", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnRegisterClicked()
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        }
    }
}
