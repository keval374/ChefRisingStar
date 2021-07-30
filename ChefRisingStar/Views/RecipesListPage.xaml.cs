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

        private void CloseDishTypeClicked(object sender, EventArgs e)
        {
            StringBuilder selectedDishTypes = new StringBuilder();

            foreach (SelectableFilter filter in _viewModel.DishTypes)
            {
                if (filter.IsSelected)
                    selectedDishTypes.Append($"{filter.Text}, ");
            }

            _viewModel.SelectedDishTypes = selectedDishTypes.ToString();
            _viewModel.IsSelectDishTypeVisible = false;
        }
        
        private void CloseDietTypeClicked(object sender, EventArgs e)
        {
            StringBuilder selectedDietTypes = new StringBuilder();

            foreach (SelectableFilter filter in _viewModel.DietTypes)
            {
                if (filter.IsSelected)
                    selectedDietTypes.Append($"{filter.Text}, ");
            }

            _viewModel.SelectedDiets = selectedDietTypes.ToString();
            _viewModel.IsSelectDietsVisible = false;
        }

        private void SelectDishTypeClicked(object sender, EventArgs e)
        {
            _viewModel.IsSelectDishTypeVisible = true;
        }
        private void FilterItemTapped(object sender, ItemTappedEventArgs e)
        {
            SelectableFilter item = e.Item as SelectableFilter;
            item.IsSelected = !item.IsSelected;
        }

    }
}