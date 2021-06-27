using ChefRisingStar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ChefRisingStar.Services
{
    public class MockRecipeDataStore : IDataStore<Recipe, int>
    {
        readonly List<Recipe> items;

        public MockRecipeDataStore()
        {
            items = new List<Recipe>()
            {
                new Recipe(1, "Recipe 1", "description 1", "ingredients", "directions" ),
                new Recipe(2, "Recipe 2", "description 2", "ingredients", "directions" ),
                new Recipe(3, "Recipe 3", "description 3", "ingredients", "directions" ),
            };
        }
        public async Task<bool> AddItemAsync(Recipe item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Recipe item)
        {
            var oldItem = items.Where((Recipe arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Recipe arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Recipe> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Recipe>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public ReadOnlyCollection<Recipe> GetItems(bool forceRefresh = false)
        {
            return items.AsReadOnly();
        }
    }
}