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
                //https://localhost:44322/api/auth/authenticate
                var userCred = new UserCred { Password = Password, Username = Username };
                string result = await RestHelper.MakePost<UserCred, string>(userCred, "api/auth/authenticate"); 

                //if (response.StatusCode == System.Net.HttpStatusCode.OK)
                //{
                //    await Shell.Current.GoToAsync($"//{nameof(RecipesListPage)}");
                //}
                //else
                //{
                //    DisplayInvalidLoginPrompt();
                //}

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
