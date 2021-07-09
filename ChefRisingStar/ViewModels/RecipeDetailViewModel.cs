using ChefRisingStar.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChefRisingStar.ViewModels
{
    public class RecipeDetailViewModel : BaseViewModel
    {
        #region Properties
        private Recipe recipe;
        public Recipe Recipe
        {
            get => recipe;
            set
            {
                if (recipe == value)
                    return;

                recipe = value;
                OnPropertyChanged();
            }
        }

        #endregion 

        #region Commands

        public Command LoadUrlCommand { get; }

        #endregion 

        #region Constructors

        public RecipeDetailViewModel()
        {
            Title = "Details";
            //LoadUrlCommand = new Command(async () => await ExecuteLoadUrlCommand());
        }

        public RecipeDetailViewModel(Recipe recipe)
        {
            Title = recipe.Title;
            Recipe = recipe;

            //LoadUrlCommand = new Command(async () => await ExecuteLoadUrlCommand());
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

        #endregion
    }
}