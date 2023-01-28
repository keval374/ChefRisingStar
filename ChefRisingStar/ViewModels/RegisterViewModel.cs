using ChefRisingStar.DTOs;
using ChefRisingStar.Helpers;
using ChefRisingStar.Models;
using ChefRisingStar.Views;
using LTDCWebservice.Utilities;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace ChefRisingStar.ViewModels
{

    public class RegisterViewModel : BaseViewModel
    {
        private string firstName;
        private string lastName;
        private string username;
        private string email;
        private string password;
        private string confirmPassword;

        public Command RegistrationCommand { get; }
        public Command SignInCommand { get; }

        public string FirstName
        {
            get => firstName;
            set
            {
                if (firstName == value)
                    return;

                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        
        public string LastName
        {
            get => lastName;
            set
            {
                if (lastName == value)
                    return;

                lastName = value;
                OnPropertyChanged("LastName");
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

           if (password != confirmPassword)
                return;

            IsBusy = true;
            
            try
            {
                User user = new User(firstName, lastName, username, email);

                string salt;
                string hashedPassword = HashUtility.HashPasword(password, out salt);
                
                user.PasswordHash = hashedPassword;
                user.Salt = salt;

                RestHelper helper = DependencyService.Get<RestHelper>();

                UserDto userDto = (UserDto)user;

                UserDto result = await helper.Post<UserDto, UserDto>(userDto, "api/Auth/CreateAccount");

                if (result.Id != 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", username + " successfully registered!", "OK");
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

        private async void OnSignInClicked()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }


    }
}

