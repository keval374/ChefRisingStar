using ChefRisingStar.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChefRisingStar.Services
{
    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class IngredientCache
    {
        private Dictionary<string, IngredientSearch> _ingredients;
        private bool _isInitialized;

        public IngredientCache()
        {
            _ingredients = new Dictionary<string, IngredientSearch>();
        }

        public bool Contains(string ingredient)
        {
            if(!_isInitialized)
            {
                LoadIngredientsFromFile();
            }

            return _ingredients.ContainsKey(ingredient);
        }

        public void Add(string ingredient, IngredientSearch ingredientSearch)
        {
            if (!_isInitialized)
            {
                LoadIngredientsFromFile();
            }

            if (string.IsNullOrEmpty(ingredient) || ingredientSearch == null || _ingredients.ContainsKey(ingredient))
                return;

            _ingredients.Add(ingredient, ingredientSearch);
        }

        public IngredientSearch Get(string ingredient)
        {
            if (!_isInitialized)
            {
                LoadIngredientsFromFile();
            }

            if (string.IsNullOrEmpty(ingredient) || !_ingredients.ContainsKey(ingredient))
                return null;

            return _ingredients[ingredient];
        }

        public void LoadIngredientsFromFile()
        {
            //TODO: this implementation
            try
            {

            }
            catch
            {

            }
            finally
            {
                _isInitialized = true;
            }

        }

        private string GetDebuggerDisplay()
        {
            return $"{_ingredients.Count} ingredients in cache";
        }

    }
}
