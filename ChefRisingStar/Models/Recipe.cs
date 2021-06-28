using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace ChefRisingStar.Models
{
    /*
    public class Recipe : List<Recipe>
    {

        public Recipe(int id, string name, string description, string ingredients, string directions)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(name));
            Ingredients = ingredients ?? throw new ArgumentNullException(nameof(name));
            Directions = directions ?? throw new ArgumentNullException(nameof(directions));
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Directions { get; set; }
        
    }
    */

    public class Recipe
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("ingredients")]
        public string ingredients { get; set; }

        [JsonProperty("url")]
        public string url { get; set; }

        [JsonProperty("image")]
        public string image { get; set; }

        [JsonProperty("cookTime")]
        public string cookTime { get; set; }

        [JsonProperty("recipeYield")]
        public string recipeYield { get; set; }

        [JsonProperty("datePublished")]
        public string datePublished { get; set; }

        [JsonProperty("prepTime")]
        public string prepTime { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}

