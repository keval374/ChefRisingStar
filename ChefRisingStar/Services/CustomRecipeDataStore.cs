using ChefRisingStar.DTOs;
using ChefRisingStar.Helpers;
using ChefRisingStar.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using static SQLite.SQLite3;
using System;

namespace ChefRisingStar.Services
{
	public class CustomRecipeDataStore : IDataStore<CustomRecipe, int>
    {
        //readonly List<CustomRecipe> items;
        List<CustomRecipe> items;
        public CustomRecipeDataStore()
		{
            items = new List<CustomRecipe>();
        }

        public async Task<bool> AddItemAsync(CustomRecipe item)
        {
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(CustomRecipe item)
        {
            var oldItem = items.Where((CustomRecipe arg) => arg.ID == item.ID).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((CustomRecipe arg) => arg.ID == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<CustomRecipe> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.ID == id));
        }

        
        public async Task<IEnumerable<CustomRecipe>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh || items.Count == 0)
            {
                RestHelper helper = DependencyService.Get<RestHelper>();
                items.Clear();

                var result = await helper.Get<CustomRecipe[]>(RestHelper.Routes.CustomRecipes);
                Console.WriteLine("result: " + result);
                items.AddRange(result);
            }

            return await Task.FromResult(items);
        }

        public ReadOnlyCollection<CustomRecipe> GetItems(bool forceRefresh = false)
        {
            return items.AsReadOnly();
        }
        


        public IEnumerable<CustomRecipe> GetSearchResults(string searchText)
        {
            return items.Where(i => i.RecipeTitle.Contains(searchText) || i.Summary.Contains(searchText));
        }
    }
}



