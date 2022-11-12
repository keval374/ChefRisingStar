using ChefRisingStar.ViewModels;
using System;
using Xamarin.Forms;

namespace ChefRisingStar.Views
{
    public partial class ManageSchoolsPage : ContentPage
    {
        SchoolViewModel _viewModel;

        public ManageSchoolsPage()
        {
            InitializeComponent();
            Title = "Manage Schools";

            BindingContext = _viewModel = new SchoolViewModel();
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;

            if (searchBar.Text.Length > 2)
            {
                _viewModel.GetSearchResults(searchBar.Text);
            }
        }
    }
}