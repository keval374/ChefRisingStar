using System;
using System.Collections.Generic;
using ChefRisingStar.ViewModels;
using Xamarin.Forms;


namespace ChefRisingStar.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            var register = new RegisterViewModel();
            this.BindingContext = register;
        }
    }
}
