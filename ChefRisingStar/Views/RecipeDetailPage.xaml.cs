using ChefRisingStar.Behaviours;
using ChefRisingStar.Helpers;
using ChefRisingStar.Models;
using ChefRisingStar.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
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
            var behave = sender as ButtonLongPressBehaviour;

            if (behave != null)
            {
                ExtendedIngredient ingredient = behave.AttachedButton.CommandParameter as ExtendedIngredient;
                _viewModel.SelectedIngredient = ingredient;
                _viewModel.IsSubstitutionVisible = true;
                _viewModel.GetSubstitutions(ingredient.NameClean);
            }
            else
            {
                Debug.WriteLine($"Error getting button for ingredient longpress sender: {sender}");
            }
        }

        private void UseSubstituteClicked(object sender, EventArgs e)
        {
            _viewModel.IsSubstitutionVisible = false;

            SubstituteIngredient[] substitutes = SubstitutionHelper.ParseSubstitution(_viewModel.SelectedSubstitution);

            if (substitutes != null)
            {
                _viewModel.ReplaceIngredient(substitutes);
                
            }            
        }

        private void CancelSubstituteClicked(object sender, EventArgs e)
        {
            _viewModel.IsSubstitutionVisible = false;
            _viewModel.SelectedIngredient = null;
        }
    }
}
