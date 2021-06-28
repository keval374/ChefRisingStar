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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipesListPage : ContentPage
    {
        RecipesViewModel _viewModel;

        public RecipesListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new RecipesViewModel();

        }
        
        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Recipe selectedRecipe)
            {
                ListView listView = (ListView)sender;
                listView.SelectedItem = null;

                await Navigation.PushAsync(new RecipeDetailPage(selectedRecipe));
            }
        }
    }
}