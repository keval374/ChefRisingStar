using ChefRisingStar.ViewModels;
using ChefRisingStar.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;

namespace ChefRisingStar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetailPage : ContentPage
    {
        public RecipeDetailPage()
        {
            InitializeComponent();
        }

        public RecipeDetailPage(Recipe recipe)
        {
            InitializeComponent();
            BindingContext = new RecipeDetailViewModel(recipe);
        }
    }
}
