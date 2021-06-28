using ChefRisingStar.Models;
using ChefRisingStar.Services;
using ChefRisingStar.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;


namespace ChefRisingStar.ViewModels
{
    public class RecipesViewModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<Recipe> Recipes { get; }

        #endregion 

        #region Commands

        public Command LoadRecipesCommand { get; }

        #endregion 

        #region Constructors

        public RecipesViewModel()
        {
            Title = "Recipes";
            Recipes = new ObservableCollection<Recipe>();

            LoadRecipesCommand = new Command(async () => await ExecuteLoadRecipesCommand());
        }

        #endregion 

        #region Methods

        async Task ExecuteLoadRecipesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                string jsonRecipes = await Client.GetStringAsync("https://openrecipes.s3.amazonaws.com/openrecipes.txt");
                char[] separator = { '\n' };
                string[] lines = jsonRecipes.Split(separator);
                Recipe[] recipes = new Recipe[lines.Length];
                
                for (int i = 0; i < 50; i++)
                {
                    Recipe recipe = JsonConvert.DeserializeObject<Recipe>(lines[i], Converter.Settings);
                    recipes[i] = recipe;
                }

                Recipes.Clear();
                foreach (var recipe in recipes)
                    Recipes.Add(recipe);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get recipes: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion 
    }

}

