using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ChefRisingStar.Models;
using ChefRisingStar.ViewModels;
using Xamarin.Forms;

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
                CustomRecipe recipe = await App.Database.GetRecipeAsync(id);
                
                _viewModel = new TempRecipeViewModel();
                _viewModel.ID = recipe.ID;
                _viewModel.RecipeTitle = recipe.RecipeTitle;
                _viewModel.Summary = recipe.Summary;
                _viewModel.Servings = recipe.Servings;
                _viewModel.ReadyInMinutes = recipe.ReadyInMinutes;
                _viewModel.SelectedCuisines = recipe.Cuisines;

                BindingContext = _viewModel;
            }
            catch (Exception ex)
            {
                await DisplayAlert("failure", "loadrecipe: " + ex, "ok", "cancel");
                Console.WriteLine("Failed to load recipe.");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                
                TempRecipeViewModel tempRecipe = (TempRecipeViewModel)BindingContext;

                CustomRecipe customRecipe = new CustomRecipe();
                customRecipe.ID = tempRecipe.ID;
                customRecipe.RecipeTitle = tempRecipe.RecipeTitle;
                customRecipe.Summary = tempRecipe.Summary;
                customRecipe.Servings = tempRecipe.Servings;
                customRecipe.ReadyInMinutes = tempRecipe.ReadyInMinutes;
                customRecipe.Cuisines = tempRecipe.SelectedCuisines;

                if (!string.IsNullOrWhiteSpace(customRecipe.RecipeTitle))
                {
                    await App.Database.SaveRecipeAsync(customRecipe);
                    //List<CustomRecipe> recipes = await App.Database.GetRecipesAsync();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("OnSaveButtonClicked failed with exception: "+ex);
                await DisplayAlert("Failure", "OnSaveButtonClicked exception: "+ex, "ok", "Cancel");
            }
            // Navigate backwards
            await Shell.Current.GoToAsync("..");

        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            TempRecipeViewModel tempRecipe = (TempRecipeViewModel)BindingContext;

            CustomRecipe customRecipe = new CustomRecipe();
            customRecipe.ID = tempRecipe.ID;
            await App.Database.DeleteRecipeAsync(customRecipe);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}

