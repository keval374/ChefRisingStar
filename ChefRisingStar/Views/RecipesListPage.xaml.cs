using ChefRisingStar.Models;
using ChefRisingStar.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChefRisingStar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipesListPage : ContentPage
    {
        RecipesViewModel _viewModel;

        public RecipesListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new RecipesViewModel();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Recipe selectedRecipe)
            {
                ListView listView = (ListView)sender;
                listView.SelectedItem = null;

                await Navigation.PushAsync(new RecipeDetailPage(selectedRecipe));
            }
        }

        private void CloseCuisineClicked(object sender, EventArgs e)
        {
            string selected = AppendSelected(_viewModel.Cuisines);

            _viewModel.SelectedCuisines = selected;
            _viewModel.IsSelectCuisineVisible = false;
        }

        private string AppendSelected(ObservableCollection<SelectableFilter> filters)
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;

            foreach (SelectableFilter filter in filters)
            {
                if (filter.IsSelected)
                {
                    if (first)
                    {
                        sb.Append($"{filter.Text}");
                        first = false;
                    }
                    else
                        sb.Append($", {filter.Text}");
                }
            }

            return sb.ToString();
        }

        private void SelectCuisineClicked(object sender, EventArgs e)
        {
            _viewModel.IsSelectCuisineVisible = true;
        }

        private void CloseDishTypeClicked(object sender, EventArgs e)
        {
            string selected = AppendSelected(_viewModel.DishTypes);

            _viewModel.SelectedDishTypes = selected;
            _viewModel.IsSelectDishTypeVisible = false;
        }

        private void CloseDietTypeClicked(object sender, EventArgs e)
        {
            string selected = AppendSelected(_viewModel.DietTypes);

            _viewModel.SelectedDiets = selected;
            _viewModel.IsSelectDietsVisible = false;
        }

        private void SelectDishTypeClicked(object sender, EventArgs e)
        {
            _viewModel.IsSelectDishTypeVisible = true;
        }
        private void FilterItemTapped(object sender, ItemTappedEventArgs e)
        {
            SelectableFilter item = e.Item as SelectableFilter;
            item.IsSelected = !item.IsSelected;
        }

    }
}