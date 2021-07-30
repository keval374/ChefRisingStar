using ChefRisingStar.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChefRisingStar.ViewModels
{
    public class RecipesViewModel : BaseViewModel
    {
        #region Properties

        private string _selectedCuisines;
        private bool _isSelectCuisineVisible;
        private string _selectedDishTypes;
        private bool _isSelectDishTypeVisible;

        private string _selectedDiets;
        private string _selectedIngredients;
        private bool _isSelectDietVisible;

        public ObservableCollection<Recipe> Recipes { get; }
        public ObservableCollection<SearchRecipe> SearchRecipes { get; set; }
        public ObservableCollection<SelectableFilter> Cuisines { get; }
        public ObservableCollection<SelectableFilter> DishTypes { get; }
        public ObservableCollection<SelectableFilter> DietTypes { get; }
        public ObservableCollection<SelectableFilter> Ingredients { get; }

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

        public string SelectedDishTypes
        {
            get { return _selectedDishTypes; }
            set { SetProperty(ref _selectedDishTypes, value); }
        }
        
        public string SelectedIngredients
        {
            get { return _selectedIngredients; }
            set { SetProperty(ref _selectedIngredients, value); }
        }

        public bool IsSelectDishTypeVisible
        {
            get { return _isSelectDishTypeVisible; }
            set { SetProperty(ref _isSelectDishTypeVisible, value); }
        }

        public string SelectedDiets
        {
            get { return _selectedDiets; }
            set { SetProperty(ref _selectedDiets, value); }
        }

        public bool IsSelectDietsVisible
        {
            get { return _isSelectDietVisible; }
            set { SetProperty(ref _isSelectDietVisible, value); }
        }
        #endregion 

        #region Commands

        public Command LoadRecipesCommand { get; }
        public Command OpenCuisinesCommand { get; }
        public Command OpenDishTypesCommand { get; }
        public Command OpenDietTypesCommand { get; }

        //public IHostingEnvironment env;

        #endregion 

        #region Constructors

        public RecipesViewModel()
        {
            Title = "Recipes";
            Recipes = new ObservableCollection<Recipe>();
            SearchRecipes = new ObservableCollection<SearchRecipe>();

            SelectableFilter[] cuisines = { new SelectableFilter("African"), new SelectableFilter("American"), new SelectableFilter("British"), new SelectableFilter("Cajun"), new SelectableFilter("Caribbean"), new SelectableFilter("Chinese"), new SelectableFilter("Eastern European"), new SelectableFilter("European"), new SelectableFilter("French"), new SelectableFilter("German"), new SelectableFilter("Greek"), new SelectableFilter("Indian"), new SelectableFilter("Irish"), new SelectableFilter("Italian"), new SelectableFilter("Japanese"), new SelectableFilter("Jewish"), new SelectableFilter("Korean"), new SelectableFilter("Latin American"), new SelectableFilter("Mediterranean"), new SelectableFilter("Mexican"), new SelectableFilter("Middle Eastern"), new SelectableFilter("Nordic"), new SelectableFilter("Southern"), new SelectableFilter("Spanish"), new SelectableFilter("Thai"), new SelectableFilter("Vietnamese") };
            Cuisines = new ObservableCollection<SelectableFilter>(cuisines);
            _selectedCuisines = string.Empty;
            OpenCuisinesCommand = new Command(OpenCuisines);

            SelectableFilter[] dishTypes = { new SelectableFilter("Main course"), new SelectableFilter("Side dish"), new SelectableFilter("Dessert"), new SelectableFilter("Appetizer"), new SelectableFilter("Salad"), new SelectableFilter("Bread"), new SelectableFilter("Breakfast"), new SelectableFilter("Soup"), new SelectableFilter("Beverage"), new SelectableFilter("Sauce"), new SelectableFilter("Marinade"), new SelectableFilter("Fingerfood"), new SelectableFilter("Snack"), new SelectableFilter("Drink") };
            DishTypes = new ObservableCollection<SelectableFilter>(dishTypes);
            _selectedDishTypes = string.Empty;
            OpenDishTypesCommand = new Command(OpenDishTypes);

            SelectableFilter[] dietTypes = { new SelectableFilter("Gluten Free"), new SelectableFilter("Ketogenic"), new SelectableFilter("Vegetarian"), new SelectableFilter("Lacto-Vegetarian"), new SelectableFilter("Ovo-Vegetarian"), new SelectableFilter("Vegan"), new SelectableFilter("Pescetarian"), new SelectableFilter("Primal"), new SelectableFilter("Whole30") };
            DietTypes = new ObservableCollection<SelectableFilter>(dietTypes);
            _selectedDiets = string.Empty;
            OpenDietTypesCommand = new Command(OpenDietTypes);

            Ingredients = new ObservableCollection<SelectableFilter>();

            _selectedCuisines = _selectedCuisines.TrimEnd(',');
            _selectedDishTypes = _selectedDishTypes.TrimEnd(',');

            LoadRecipesCommand = new Command(async () => await ExecuteLoadRecipesCommand(_selectedCuisines, _selectedDishTypes));
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

        private void OpenDishTypes()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            IsSelectDishTypeVisible = true;

            IsBusy = false;
        }

        private void OpenDietTypes()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            IsSelectDietsVisible = true;

            IsBusy = false;
        }

        async Task ExecuteLoadRecipesCommand(string _selectedCuisines, string _selectedDishTypes)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                //Recipes = new ObservableCollection<Recipe>();
                SearchRecipes = new ObservableCollection<SearchRecipe>();

                _selectedIngredients = "beef"; // todo: remove temp only for demo

                string api = $"https://api.spoonacular.com/recipes/complexSearch?apiKey=4f1ec6d27f5240a18921a16686659406&instructionsRequired&number=5";

                if (!string.IsNullOrEmpty(_selectedCuisines))
                    api += $"&cuisine={_selectedCuisines}";

                if (!string.IsNullOrEmpty(_selectedDishTypes))
                    api += $"&type={_selectedDishTypes}";

                if (!string.IsNullOrEmpty(_selectedDiets))
                    api += $"&diet={_selectedDiets}";
                
                if (!string.IsNullOrEmpty(_selectedIngredients))
                    api += $"&includeIngredients={_selectedIngredients}";

                string jsonRecipesResults = await Client.GetStringAsync(api);

                Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(jsonRecipesResults);

                var jArray = jObject["results"] as Newtonsoft.Json.Linq.JArray;

                foreach (Newtonsoft.Json.Linq.JObject item in jArray)
                {
                    string sResults = item.ToString();

                    try
                    {
                        SearchRecipe recipeResults = JsonConvert.DeserializeObject<SearchRecipe>(sResults, Converter.Settings);
                        SearchRecipes.Add(recipeResults);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"Unable to get deserialize recipe: {e.Message}");
                    }
                }

                foreach (var recipeResult in SearchRecipes)
                {
                    string jsonRecipesInfo = await Client.GetStringAsync($"https://api.spoonacular.com/recipes/{recipeResult.Id}/information?apiKey=4f1ec6d27f5240a18921a16686659406&includeNutrition=false");
                    string s = jsonRecipesInfo.ToString();
                    Recipe recipe = JsonConvert.DeserializeObject<Recipe>(s);
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

