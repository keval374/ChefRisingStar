using ChefRisingStar.Models;
using System.Diagnostics;
using System;
using Xamarin.Forms;
using ChefRisingStar.ViewModels;

namespace ChefRisingStar.Views
{
    public partial class ManageUsersPage : ContentPage
    {
        UsersViewModel _viewModel;
        public ManageUsersPage()
        {
            InitializeComponent();
            Title = "Manage Users";
            BindingContext = _viewModel = new UsersViewModel();
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            _viewModel.GetSearchResults(searchBar.Text);
        }
    }
}