using ChefRisingStar.Models;
using ChefRisingStar.Services;
using ChefRisingStar.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Web;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;

namespace ChefRisingStar.ViewModels
{
    public class RecipesViewModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<Recipe> Recipes { get; }

        #endregion 

        #region Commands

        public Command LoadRecipesCommand { get; }

        public IHostingEnvironment env;

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
                string api = "https://api.spoonacular.com/recipes/random?apiKey=4f1ec6d27f5240a18921a16686659406&number=1&tags=vegetarian";
                string jsonRecipes = await Client.GetStringAsync(api);
                //string jsonRecipes = "";
                //string file = "recipes.json";

                //using (var reader = new System.IO.StreamReader(stream))
                //{
                //    jsonRecipes = reader.ReadToEnd();
                //}

                //string jsonRecipes = File.ReadAllText(@"~/bin/recipes.json");

                Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(jsonRecipes);

                var jArray = jObject["recipes"] as Newtonsoft.Json.Linq.JArray;

                foreach (Newtonsoft.Json.Linq.JObject item in jArray)
                {
                    string s = item.ToString();
                    Recipe recipe = JsonConvert.DeserializeObject<Recipe>(s, Converter.Settings);
                    Recipes.Add(recipe);
                }
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

