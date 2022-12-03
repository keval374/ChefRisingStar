using ChefRisingStar.Models;
using ChefRisingStar.ViewModels;
using System;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace ChefRisingStar.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class RecipeEntryPage : ContentPage
    {
        TempRecipeViewModel _viewModel;

        public string ItemId
        {
            set
            {
                LoadRecipe(value);
            }
        }

        public RecipeEntryPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new TempRecipeViewModel();
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

        private void CloseDishTypeClicked(object sender, EventArgs e)
        {
            string selected = AppendSelected(_viewModel.DishTypes);

            _viewModel.SelectedDishTypes = selected;
            _viewModel.IsSelectDishTypeVisible = false;
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

        private string AppendSelected(ObservableCollection<SelectableFilter> filters)
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;

            foreach (SelectableFilter filter in filters)
            {
                if (filter.IsSelected)
                {
                    if (first)
                    {
                        sb.Append($"{filter.Text}");
                        first = false;
                    }
                    else
                        sb.Append($", {filter.Text}");
                }
            }

            return sb.ToString();
        }

        async void LoadRecipe(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the recipe and set it as the BindingContext of the page.
                TempRecipeViewModel recipe = await App.Database.GetRecipeAsync(id);
                BindingContext = recipe;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load recipe.");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var recipe = (TempRecipeViewModel)BindingContext;

            if (!string.IsNullOrWhiteSpace(recipe.RecipeTitle))
            {
                await App.Database.SaveRecipeAsync(recipe);
                var recipes = App.Database.GetRecipesAsync();
            }
            
            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var recipe = (TempRecipeViewModel)BindingContext;
            await App.Database.DeleteRecipeAsync(recipe);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}

