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
}
