using ChefRisingStar.Models;
using SQLite;
using Xamarin.Forms;
using ChefRisingStar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChefRisingStar.ViewModels;

namespace ChefRisingStar.Services
{
    public class TempRecipeDatabase
    {
        readonly SQLiteAsyncConnection database;

        public TempRecipeDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TempRecipeViewModel>().Wait();
        }

        public Task<List<TempRecipeViewModel>> GetRecipesAsync()
        {
            //Get all Recipes.
            return database.Table<TempRecipeViewModel>().ToListAsync();
        }

        public Task<TempRecipeViewModel> GetRecipeAsync(int id)
        {
            // Get a specific recipes.
            return database.Table<TempRecipeViewModel>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveRecipeAsync(TempRecipeViewModel recipe)
        {
            if (recipe.ID != 0)
            {
                // Update an existing recipe.
                return database.UpdateAsync(recipe);
            }
            else
            {
                // Save a new recipe.
                return database.InsertAsync(recipe);
            }
        }

        public Task<int> DeleteRecipeAsync(TempRecipeViewModel recipe)
        {
            // Delete a recipe.
            return database.DeleteAsync(recipe);
        }
    }
}


