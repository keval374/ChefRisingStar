using ChefRisingStar.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using Xamarin.Forms;

namespace ChefRisingStar.ViewModels
{
    public class RecipeDetailViewModel : BaseViewModel
    {
        #region Members
        private bool _isSubstitutionVisible;
        private Recipe _recipe;
        #endregion Members

        #region Properties

        public Recipe Recipe
        {
            get => _recipe;
            set
            {
                if (_recipe == value)
                    return;

                _recipe = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Substitutions { get; protected set; }

        public bool IsSubstitutionVisible
        {
            get => _isSubstitutionVisible;
            set
            {
                _isSubstitutionVisible = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Step> Instructions { get; protected set; }

        #endregion 

        #region Commands

        public Command LoadUrlCommand { get; }

        #endregion 

        #region Constructors

        public RecipeDetailViewModel(Recipe recipe)
        {
            Title = recipe.Title;
            Recipe = recipe;

            Instructions = new ObservableCollection<Step>();
            Substitutions = new ObservableCollection<string>();

            foreach (AnalyzedInstruction ins in Recipe.AnalyzedInstructions)
            {
                foreach (Step step in ins.Steps)
                    Instructions.Add(step);
            }

            GetSubstitutions();
        }

        #endregion 

        #region Methods

        /*
        async Task ExecuteLoadUrlCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Launcher.OpenAsync(new Uri(recipe.url));
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
        }*/

        private async void GetSubstitutions()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //client.BaseAddress = new Uri("https://api.spoonacular.com/food/ingredients/substitutes");
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Add an Accept header for JSON format.
                    //var streamTask = client.GetStreamAsync("?apiKey=61f0c9888f5542a6b3604a030707b8ad&ingredientName=butter");

                    //var substitution = await JsonSerializer.DeserializeAsync<Substitution>(await streamTask);

                    string strResponse = "{\"status\":\"success\",\"ingredient\":\"butter\",\"substitutes\":[\"1 cup = 7 / 8 cup shortening and 1 / 2 tsp salt\",\"1 cup = 7 / 8 cup vegetable oil + 1 / 2 tsp salt\",\"1 / 2 cup = 1 / 4 cup buttermilk +1 / 4 cup unsweetened applesauce\",\"1 cup = 1 cup margarine\"],\"message\":\"Found 4 substitutes for the ingredient.\"}";
                    var substitution = JsonSerializer.Deserialize<Substitution>(strResponse);

                    foreach (string s in substitution.Substitutes)
                    {
                        Substitutions.Add(s);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error getting substitutions: {ex}");
                }
            }
        }

        #endregion
    }
}