using ChefRisingStar.Models;

using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using ChefRisingStar.ViewModels;
using Xamarin.Forms.PlatformConfiguration;
using ChefRisingStar.Services;

namespace ChefRisingStar.Views
{
    public partial class CustomRecipePage : ContentPage
    {
        TempRecipeViewModel _viewModel;
        IDataStore<CustomRecipe, int> ds;
        
        public CustomRecipePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new TempRecipeViewModel();
            Title = _viewModel.Title;
            //ds = DependencyService.Get<IDataStore<CustomRecipe, int>>();
            ds = new CustomRecipeDataStore();
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the recipes from the database, and set them as the
            // data source for the CollectionView.
            //collectionView.ItemsSource = await App.Database.GetRecipesAsync();
            try
            {
                //IEnumerable<CustomRecipe> res;
                List<CustomRecipe> res;
                await DisplayAlert("alert", "OnAppearing: ", "ok", "Cancel");
                res = (await ds.GetItemsAsync(true)).ToList();
                await DisplayAlert("alert", "res: "+res+" size: "+res.Count(), "ok", "Cancel");

                collectionView.ItemsSource = res;
            }
            catch(Exception ex)
            {
                await DisplayAlert("error", "OnAppearing: "+ex, "ok", "Cancel");
            }
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the RecipeEntryPage.
            await Shell.Current.GoToAsync(nameof(RecipeEntryPage));
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the RecipeEntryPage, passing the ID as a query parameter.
                
                CustomRecipe recipe = (CustomRecipe)e.CurrentSelection.FirstOrDefault();
                await DisplayAlert("alert", "OnSelectionChanged: " + recipe, "ok", "Cancel");
                await Shell.Current.GoToAsync($"{nameof(RecipeEntryPage)}?{nameof(RecipeEntryPage.ItemId)}={recipe.ID}");
            }
        }
    }

}

