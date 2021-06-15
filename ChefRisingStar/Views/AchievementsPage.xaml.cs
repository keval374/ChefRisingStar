using ChefRisingStar.Models;
using ChefRisingStar.ViewModels;
using ChefRisingStar.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChefRisingStar.Views
{
    public partial class AchievementsPage : ContentPage
    {
        AchievementsViewModel _viewModel;

        public AchievementsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new AchievementsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}