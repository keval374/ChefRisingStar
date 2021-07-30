using ChefRisingStar.Views;
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

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string userauth = username + ":" + password;
                    client.DefaultRequestHeaders.Add("ContentType", "application/json");

                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(userauth);
                    string val = System.Convert.ToBase64String(plainTextBytes);
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + val);

                    HttpResponseMessage response = client.GetAsync("https://chefrisingstar-api.mybluemix.net/").Result;

                    response = client.GetAsync("https://chefrisingstar-api.mybluemix.net/api/login/").Result;

                    if (response.StatusCode.ToString() == "OK")
                    {
                        await Shell.Current.GoToAsync($"//{nameof(RecipesListPage)}");
                    }
                    else
                    {
                        DisplayInvalidLoginPrompt();
                    }

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

        }

        private async void OnRegisterClicked()
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        }
    }
}
