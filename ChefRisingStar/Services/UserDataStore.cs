using ChefRisingStar.DTOs;
using ChefRisingStar.Helpers;
using ChefRisingStar.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using static SQLite.SQLite3;

namespace ChefRisingStar.Services
{
    public class UserDataStore : IDataStore<User, int>
    {
        readonly List<User> items;

        public UserDataStore()
        {
            items = new List<User>();
        }

        public async Task<bool> AddItemAsync(User item)
        {
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(User item)
        {
            var oldItem = items.Where((User arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((User arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<User> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<User>> GetItemsAsync(bool forceRefresh = false)
        {
            if(forceRefresh || items.Count == 0)
            {
                RestHelper helper = DependencyService.Get<RestHelper>();
                items.Clear();
                
                var result = await helper.Get<User[]>(RestHelper.Routes.Users);
                items.AddRange(result);
            }

            return await Task.FromResult(items);
        }

        public ReadOnlyCollection<User> GetItems(bool forceRefresh = false)
        {
            return items.AsReadOnly();
        }

        public IEnumerable<User> GetSearchResults(string searchText)
        {
            return items.Where(i => i.FirstName.Contains(searchText) || i.LastName.Contains(searchText) || i.UserName.Contains(searchText));
        }
    }
}