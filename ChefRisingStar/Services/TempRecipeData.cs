using ChefRisingStar.Models;
using SQLite;
using Xamarin.Forms;
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
            database.CreateTableAsync<CustomRecipe>().Wait();
        }

        public Task<List<CustomRecipe>> GetRecipesAsync()
        {
            //Get all Recipes.
            return database.Table<CustomRecipe>().ToListAsync();
        }

        public Task<CustomRecipe> GetRecipeAsync(int id)
        {
            // Get a specific recipes.
            return database.Table<CustomRecipe>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveRecipeAsync(CustomRecipe recipe)
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

        public Task<int> DeleteRecipeAsync(CustomRecipe recipe)
        {
            // Delete a recipe.
            return database.DeleteAsync(recipe);
        }
    }
}


