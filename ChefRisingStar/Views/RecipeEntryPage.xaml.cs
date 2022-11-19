using System;
using System.Collections.Generic;
using ChefRisingStar.Models;
using ChefRisingStar.ViewModels;
using Xamarin.Forms;

namespace ChefRisingStar.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class RecipeEntryPage : ContentPage
    {

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
            // Set the BindingContext of the page to a new Recipe.
            BindingContext = new TempRecipeDetails();
        }

        async void LoadRecipe(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the recipe and set it as the BindingContext of the page.
                TempRecipeDetails recipe = await App.Database.GetRecipeAsync(id);
                BindingContext = recipe;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load recipe.");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var recipe = (TempRecipeDetails)BindingContext;
            if (!string.IsNullOrWhiteSpace(recipe.Title))
            {
                await App.Database.SaveRecipeAsync(recipe);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var recipe = (TempRecipeDetails)BindingContext;
            await App.Database.DeleteRecipeAsync(recipe);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}

