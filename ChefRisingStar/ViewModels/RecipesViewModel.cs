using ChefRisingStar.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChefRisingStar.ViewModels
{
    public class RecipesViewModel : BaseViewModel
    {
        #region Properties

        private string _selectedCuisines;
        private bool _isSelectCuisineVisible;
        
        public ObservableCollection<Recipe> Recipes { get; }
        public ObservableCollection<SelectableFilter> Cuisines { get; }
        
        public string SelectedCuisines
        {
            get { return _selectedCuisines; }
            set { SetProperty(ref _selectedCuisines, value); }
        }

        public bool IsSelectCuisineVisible
        {
            get { return _isSelectCuisineVisible; }
            set { SetProperty(ref _isSelectCuisineVisible, value); }
        }

        #endregion 

        #region Commands

        public Command LoadRecipesCommand { get; }
        public Command OpenCuisinesCommand { get; }

        public IHostingEnvironment env;

        #endregion 

        #region Constructors

        public RecipesViewModel()
        {
            Title = "Recipes";
            Recipes = new ObservableCollection<Recipe>();

            SelectableFilter[] cuisines = { new SelectableFilter("African"), new SelectableFilter("American"), new SelectableFilter("British"), new SelectableFilter("Cajun"), new SelectableFilter("Caribbean"), new SelectableFilter("Chinese"), new SelectableFilter("Eastern European"), new SelectableFilter("European"), new SelectableFilter("French"), new SelectableFilter("German"), new SelectableFilter("Greek"), new SelectableFilter("Indian"), new SelectableFilter("Irish"), new SelectableFilter("Italian"), new SelectableFilter("Japanese"), new SelectableFilter("Jewish"), new SelectableFilter("Korean"), new SelectableFilter("Latin American"), new SelectableFilter("Mediterranean"), new SelectableFilter("Mexican"), new SelectableFilter("Middle Eastern"), new SelectableFilter("Nordic"), new SelectableFilter("Southern"), new SelectableFilter("Spanish"), new SelectableFilter("Thai"), new SelectableFilter("Vietnamese") };
            Cuisines = new ObservableCollection<SelectableFilter>(cuisines);
            _selectedCuisines = string.Empty;
            OpenCuisinesCommand = new Command(OpenCuisines);

            LoadRecipesCommand = new Command(async () => await ExecuteLoadRecipesCommand());

        }

        #endregion 

        #region Methods

        private void OpenCuisines()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            IsSelectCuisineVisible = true;

            //OnPropertyChanged("SelectedCuisines");

            IsBusy = false;
        }



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

