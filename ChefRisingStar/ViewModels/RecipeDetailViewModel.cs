using ChefRisingStar.Models;
using System.Collections.ObjectModel;
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

            foreach (AnalyzedInstruction ins in Recipe.AnalyzedInstructions)
            {
                foreach (Step step in ins.Steps)
                    Instructions.Add(step);
            }
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