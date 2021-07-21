using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Diagnostics;
using System.Globalization;

namespace ChefRisingStar.Models
{
    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class FoodPrint
    {
        [JsonProperty("ingredient")]
        public string Ingredient { get; set; }

        [JsonProperty("land_usage")]
        public long LandUsage { get; set; }

        [JsonProperty("farming")]
        public long Farming { get; set; }

        [JsonProperty("animal_feed")]
        public long AimalFeed { get; set; }

        [JsonProperty("processing")]
        public long Processing { get; set; }

        [JsonProperty("transport")]
        public long Transport { get; set; }

        [JsonProperty("retail")]
        public long Retail { get; set; }

        [JsonProperty("packaging")]
        public long Packaging { get; set; }

        [JsonProperty("waste")]
        public long Waste { get; set; }

        [JsonProperty("totalCO2")]
        public long TotalCO2 { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"{Ingredient} - {TotalCO2}";
        }
    }

    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class AnnualFoodPrint
    {
        [JsonProperty("ingredients")]
        public string Ingredients { get; set; }

        [JsonProperty("totalCO2")]
        public long AnnualCO2 { get; set; }

        [JsonProperty("car_miles")]
        public long CarMiles { get; set; }

        [JsonProperty("water_usage")]
        public long WaterUsage { get; set; }

        [JsonProperty("parking_space")]
        public long ParkingSpace { get; set; }


        private string GetDebuggerDisplay()
        {
            return $"{Ingredients} - {AnnualCO2}";
        }
    }

    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class OtherFoodPrint
    {
        [JsonProperty("ingredient")]
        public string Ingredient { get; set; }

        [JsonProperty("product_emission")]
        public long ProductEmission { get; set; }

        [JsonProperty("transport_emission")]
        public string TransportEmission { get; set; }

        [JsonProperty("waste_emission")]
        public string WasteEmission { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"{Ingredient} - {ProductEmission}";
        }
    }
}
