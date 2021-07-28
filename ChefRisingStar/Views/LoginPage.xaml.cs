using ChefRisingStar.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;

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

            Email.Completed += (object sender, EventArgs e) =>
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