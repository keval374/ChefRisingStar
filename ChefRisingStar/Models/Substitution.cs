using System.Diagnostics;
using System.Text.Json.Serialization;

namespace ChefRisingStar.Models
{
    public class Substitution
    {
        [JsonPropertyName("ingredient")]
        public string Ingredient { get; set; }

        [JsonPropertyName("substitutes")]
        public string[] Substitutes { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class IngredientSearch
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("aisle")]
        public string Aisle { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        public string ImageSrc
        {
            get { return $"https://spoonacular.com/cdn/ingredients_100x100/{Image}"; }
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("possibleUnits")]
        public string[] PossibleUnits { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"{Id} - {Name}";
        }
    }

    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class SubstituteIngredient
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        public string ImageSrc
        {
            get { return $"https://spoonacular.com/cdn/ingredients_100x100/{Image}"; }
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("conversion")]
        public string Conversion { get; set; }

        public override string ToString()
        {
            //return $"{Amount} {Unit} - {Name}";
            return $"{Name}";
        }

        private string GetDebuggerDisplay()
        {
            return $"{Amount} {Unit} - {Name}";
        }
    }
}
