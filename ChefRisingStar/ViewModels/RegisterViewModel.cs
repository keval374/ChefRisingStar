using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using ChefRisingStar.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ChefRisingStar.ViewModels
{

    public class RegisterViewModel : BaseViewModel
    {
        private string fullName;
        private string username;
        private string email;
        private string password;
        private string confirmPassword;

        public Command RegistrationCommand { get; }
        public Command SignInCommand { get; }
        
        public string FullName
        {
            get => fullName;
            set
            {
                if (fullName == value)
                    return;

                fullName = value;
                OnPropertyChanged("FullName");
            }
        }

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

        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                if (confirmPassword == value)
                    return;

                confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }
        }



        public RegisterViewModel()
        {
            RegistrationCommand = new Command(OnRegistrationClicked);
            SignInCommand = new Command(OnSignInClicked);
        }

        private async void OnRegistrationClicked()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (password != confirmPassword)
                return;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var my_jsondata = new
                    {
                        username = username,
                        password = password,
                        email = email
                    };
                    string json_data = JsonConvert.SerializeObject(my_jsondata);

                    var response = await client.PostAsync(
                        "https://chefrisingstar-api.mybluemix.net/api/user/registry",
                        new StringContent(json_data, Encoding.UTF8, "application/json"));

                    if (response.StatusCode.ToString() == "OK")
                    {
                        await Application.Current.MainPage.DisplayAlert("Success", username+" successfully registered!", "OK");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "something went wrong, try again", "OK");
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error during registration: {ex}");
                    await Application.Current.MainPage.DisplayAlert("API Error:", ex.Message, "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        

        private async void OnSignInClicked()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }
          
            
    }
}

