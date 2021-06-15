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
    public class AchievementsViewModel : BaseViewModel
    {
        private Achievement _selectedItem;

        public ObservableCollection<Achievement> Achievements { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Achievement> ItemTapped { get; }

        public IDataStore<Achievement> AchievementDataStore => DependencyService.Get<IDataStore<Achievement>>();

        public AchievementsViewModel()
        {
            Title = "Achievements";
            Achievements = new ObservableCollection<Achievement>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Achievement>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Achievements.Clear();
                var items = await AchievementDataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Achievements.Add(item);
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
        }

        public Achievement SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Achievement achievement)
        {
            if (achievement == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //TODO: This detail
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={achievement.Id}");
        }
    }
}