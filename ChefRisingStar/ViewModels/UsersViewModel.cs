using ChefRisingStar.Models;
using ChefRisingStar.Services;
using ChefRisingStar.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChefRisingStar.ViewModels
{
    public class UsersViewModel : BaseDataViewModel<User, int>
    {
        private User _selectedItem;

        //public ObservableCollection<User> Users { get; private set; }

        private List<User> _users;
        public List<User> Users
        {
            get
            {
                return _users;
            }
            set
            {    
                SetProperty(ref _users, value);
            }
        }

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<User> ItemTapped { get; }

        public override IDataStore<User, int> DataStore { get; protected set; }

        public UsersViewModel()
        {
            Title = "Users";
            Users = new List<User>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<User>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            DataStore = DependencyService.Get<IDataStore<User, int>>();
            //ExecuteLoadItemsCommand();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Users.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Users.Add(item);
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

        public User SelectedItem
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

        async void OnItemSelected(User item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //TODO: Implement UserDetailPage & UserDetailViewModel
            //await Shell.Current.GoToAsync($"{nameof(UserDetailPage)}?{nameof(UserDetailViewModel.Id)}={user.Id}");
        }

        public void GetSearchResults(string searchText)
        {
            //Users.Clear();

            //foreach (var result in DataStore.GetSearchResults(searchText))
            //{
            //    Users.Add(result);
            //}

            Users = DataStore.GetSearchResults(searchText).ToList();
        }

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            Users = DataStore.GetSearchResults(query).ToList();
        });

    }
}