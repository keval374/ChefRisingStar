using ChefRisingStar.Models;
using ChefRisingStar.Services;
using ChefRisingStar.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChefRisingStar.ViewModels
{
    public class RecipesViewModel : BaseViewModel
    {
        private Recipe _selectedItem;
        public ObservableCollection<Recipe> recipesList { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Achievement> ItemTapped { get; }
        public IDataStore<Recipe, int> RecipeDataStore => DependencyService.Get<IDataStore<Recipe, int>>();

        public RecipesViewModel()
        {
            
            Title = "RecipesList";
            recipesList = new ObservableCollection<Recipe>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                recipesList.Clear();
                var items = await RecipeDataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    recipesList.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;

            //recipesList.ItemSource = null;
            //recipesList.ItemSource = App.AllRecipes;
        }

        public Recipe SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(Recipe recipe)
        {
            if (recipe == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //TODO: This detail
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={recipe.Id}");
        }

        /*
        async void Add_Clicked(object sender, System.EventArgs e)
        {
            var editPage = new RecipeEditPage();

            var editNavPage = new NavigationPage(editPage)
            {
                BarBackgroundColor = Color.FromHex("#01487E"),
                BarTextColor = Color.White
            };

            await Navigation.PushModalAsync(editNavPage);
        }
        */
    }
}

