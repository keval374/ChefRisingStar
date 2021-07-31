using ChefRisingStar.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace ChefRisingStar.Services
{
    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class IngredientCache
    {
        private Dictionary<string, IngredientSearch> _ingredients;
        private bool _isInitialized;
        private bool _isInitializing;

        public IngredientCache()
        {
            _ingredients = new Dictionary<string, IngredientSearch>();
        }

        public bool Contains(string ingredient)
        {
            if (!_isInitialized)
            {
                LoadIngredientsFromFile();
            }

            ingredient = ingredient.ToLower();

            return _ingredients.ContainsKey(ingredient);
        }

        public void Add(string ingredient, IngredientSearch ingredientSearch)
        {
            if (string.IsNullOrEmpty(ingredient) || ingredientSearch == null)
                return;

            ingredient = ingredient.ToLower();

            if (_ingredients.ContainsKey(ingredient))
                return;

            _ingredients.Add(ingredient, ingredientSearch);
        }

        public IngredientSearch Get(string ingredient)
        {
            if (!_isInitialized)
            {
                LoadIngredientsFromFile();
            }

            ingredient = ingredient.ToLower();

            if (string.IsNullOrEmpty(ingredient) || !_ingredients.ContainsKey(ingredient))
                return null;

            return _ingredients[ingredient];
        }

        public void LoadIngredientsFromFile()
        {
            try
            {
                var assembly = typeof(Views.RecipeDetailPage).GetTypeInfo().Assembly;
                //string[] resources = assembly.GetManifestResourceNames();
                Stream stream = assembly.GetManifestResourceStream("ChefRisingStar.sample.IngredientSearch.json");

                using (var reader = new StreamReader(stream))
                {

                    var json = reader.ReadToEnd();
                    var data = JsonSerializer.Deserialize<IngredientSearch[]>(json);

                    foreach (IngredientSearch i in data)
                    {
                        Add(i.Name, i);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading ingredient cache: {ex}");
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
