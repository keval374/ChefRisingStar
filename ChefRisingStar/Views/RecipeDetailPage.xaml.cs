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
        public RecipeDetailPage(Recipe recipe)
        {
            InitializeComponent();
            BindingContext = new RecipeDetailViewModel(recipe);
        }

        private void IngredientLongPressed(object sender, EventArgs e)
        {
        }
    }
}
