using ChefRisingStar.Models;
using ChefRisingStar.ViewModels;
using System;
using System.Text;
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

        private void CloseCuisineClicked(object sender, EventArgs e)
        {
            StringBuilder selectedCuisined = new StringBuilder();

            foreach (SelectableFilter filter in _viewModel.Cuisines)
            {
                if (filter.IsSelected)
                    selectedCuisined.Append($"{filter.Text}, ");
            }

            _viewModel.SelectedCuisines = selectedCuisined.ToString();
            _viewModel.IsSelectCuisineVisible = false;
        }

        private void SelectCuisineClicked(object sender, EventArgs e)
        {
            _viewModel.IsSelectCuisineVisible = true;
        }

        private void cuisineList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            SelectableFilter item = e.Item as SelectableFilter;
            item.IsSelected = !item.IsSelected;
        }
    }
}