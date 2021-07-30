using ChefRisingStar.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChefRisingStar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            var login = new LoginViewModel();
            this.BindingContext = login;
            login.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid username or password, Please try again", "OK");

            InitializeComponent();

            Username.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            Password.Completed += (object sender, EventArgs e) =>
            {
                login.LoginCommand.Execute(null);
            };
        }
    }
}