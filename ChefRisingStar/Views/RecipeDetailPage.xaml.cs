using ChefRisingStar.Models;
using ChefRisingStar.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChefRisingStar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetailPage : ContentPage
    {
        private RecipeDetailViewModel _viewModel;

        public RecipeDetailPage(Recipe recipe)
        {
            InitializeComponent();
            BindingContext = _viewModel = new RecipeDetailViewModel(recipe);
        }

        private void IngredientLongPressed(object sender, EventArgs e)
        {
            _viewModel.IsSubstitutionVisible = true;
        }

        private void UseSubstituteClicked(object sender, EventArgs e)
        {
            _viewModel.IsSubstitutionVisible = false;
        }

        private void CancelSubstituteClicked(object sender, EventArgs e)
        {
            _viewModel.IsSubstitutionVisible = false;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}
