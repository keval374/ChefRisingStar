using System;
using ChefRisingStar.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Newtonsoft.Json;
using SQLite;
using Xamarin.Forms.Xaml;


namespace ChefRisingStar.ViewModels
{
	public class TempRecipeViewModel : BaseViewModel
	{
        private string _selectedCuisines;
        private bool _isSelectCuisineVisible;

        private string _selectedDishTypes;
        private bool _isSelectDishTypeVisible;

        public ObservableCollection<SelectableFilter> Cuisines { get; }
        public ObservableCollection<SelectableFilter> DishTypes { get; }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        //[JsonProperty("title")]
        public string RecipeTitle { get; set; }

        //[JsonProperty("readyInMinutes")]
        public long ReadyInMinutes { get; set; }

        //[JsonProperty("servings")]
        public long Servings { get; set; }

        //[JsonProperty("instructions")]
        public string Instructions { get; set; }

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

        public bool IsSelectDishTypeVisible
        {
            get { return _isSelectDishTypeVisible; }
            set { SetProperty(ref _isSelectDishTypeVisible, value); }
        }

        public Command OpenCuisinesCommand { get; }
        public Command OpenDishTypesCommand { get; }


        public TempRecipeViewModel()
		{
            SelectableFilter[] cuisines = { new SelectableFilter("African"), new SelectableFilter("American"), new SelectableFilter("British"), new SelectableFilter("Cajun"), new SelectableFilter("Caribbean"), new SelectableFilter("Chinese"), new SelectableFilter("Eastern European"), new SelectableFilter("European"), new SelectableFilter("French"), new SelectableFilter("German"), new SelectableFilter("Greek"), new SelectableFilter("Indian"), new SelectableFilter("Irish"), new SelectableFilter("Italian"), new SelectableFilter("Japanese"), new SelectableFilter("Jewish"), new SelectableFilter("Korean"), new SelectableFilter("Latin American"), new SelectableFilter("Mediterranean"), new SelectableFilter("Mexican"), new SelectableFilter("Middle Eastern"), new SelectableFilter("Nordic"), new SelectableFilter("Southern"), new SelectableFilter("Spanish"), new SelectableFilter("Thai"), new SelectableFilter("Vietnamese") };
            Cuisines = new ObservableCollection<SelectableFilter>(cuisines);
            _selectedCuisines = string.Empty;
            OpenCuisinesCommand = new Command(OpenCuisines);
            _selectedCuisines = _selectedCuisines.TrimEnd(',');

            SelectableFilter[] dishTypes = { new SelectableFilter("Main course"), new SelectableFilter("Side dish"), new SelectableFilter("Dessert"), new SelectableFilter("Appetizer"), new SelectableFilter("Salad"), new SelectableFilter("Bread"), new SelectableFilter("Breakfast"), new SelectableFilter("Soup"), new SelectableFilter("Beverage"), new SelectableFilter("Sauce"), new SelectableFilter("Marinade"), new SelectableFilter("Fingerfood"), new SelectableFilter("Snack"), new SelectableFilter("Drink") };
            DishTypes = new ObservableCollection<SelectableFilter>(dishTypes);
            _selectedDishTypes = string.Empty;
            OpenDishTypesCommand = new Command(OpenDishTypes);
            _selectedDishTypes = _selectedDishTypes.TrimEnd(',');

        }

        private void OpenCuisines()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            IsSelectCuisineVisible = true;

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

    }
}

