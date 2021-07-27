using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Diagnostics;
using System.Globalization;

namespace ChefRisingStar.Models
{    
    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class Recipe
    {
        [JsonProperty("vegetarian")]
        public bool Vegetarian { get; set; }

        [JsonProperty("vegan")]
        public bool Vegan { get; set; }

        [JsonProperty("glutenFree")]
        public bool GlutenFree { get; set; }

        [JsonProperty("dairyFree")]
        public bool DairyFree { get; set; }

        [JsonProperty("veryHealthy")]
        public bool VeryHealthy { get; set; }

        [JsonProperty("cheap")]
        public bool Cheap { get; set; }

        [JsonProperty("veryPopular")]
        public bool VeryPopular { get; set; }

        [JsonProperty("sustainable")]
        public bool Sustainable { get; set; }

        [JsonProperty("weightWatcherSmartPoints")]
        public long WeightWatcherSmartPoints { get; set; }

        [JsonProperty("gaps")]
        public string Gaps { get; set; }

        [JsonProperty("lowFodmap")]
        public bool LowFodmap { get; set; }

        [JsonProperty("aggregateLikes")]
        public long AggregateLikes { get; set; }

        [JsonProperty("spoonacularScore")]
        public long SpoonacularScore { get; set; }

        [JsonProperty("healthScore")]
        public long HealthScore { get; set; }

        [JsonProperty("creditsText")]
        public string CreditsText { get; set; }

        [JsonProperty("license")]
        public string License { get; set; }

        [JsonProperty("sourceName")]
        public string SourceName { get; set; }

        [JsonProperty("pricePerServing")]
        public double PricePerServing { get; set; }

        [JsonProperty("extendedIngredients")]
        public ExtendedIngredient[] ExtendedIngredients { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("readyInMinutes")]
        public long ReadyInMinutes { get; set; }

        [JsonProperty("servings")]
        public long Servings { get; set; }

        [JsonProperty("sourceUrl")]
        public Uri SourceUrl { get; set; }

        [JsonProperty("image")]
        public Uri Image { get; set; }

        [JsonProperty("imageType")]
        public string ImageType { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("cuisines")]
        public string[] Cuisines { get; set; }

        [JsonProperty("dishTypes")]
        public string[] DishTypes { get; set; }

        [JsonProperty("diets")]
        public string[] Diets { get; set; }

        [JsonProperty("occasions")]
        public string[] Occasions { get; set; }

        [JsonProperty("instructions")]
        public string Instructions { get; set; }

        [JsonProperty("analyzedInstructions")]
        public AnalyzedInstruction[] AnalyzedInstructions { get; set; }

        [JsonProperty("originalId")]
        public object OriginalId { get; set; }

        [JsonProperty("spoonacularSourceUrl")]
        public Uri SpoonacularSourceUrl { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"{Id} - {Title}";
        }
    }

    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class AnalyzedInstruction
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("steps")]
        public Step[] Steps { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"{Name} - # steps {Steps.Length}";
        }
    }

    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class Step
    {
        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("step")]
        public string Instruction { get; set; }

        [JsonProperty("ingredients")]
        public Ent[] Ingredients { get; set; }

        [JsonProperty("equipment")]
        public Ent[] Equipment { get; set; }

        [JsonProperty("length", NullValueHandling = NullValueHandling.Ignore)]
        public Length Length { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"#{Number} : {Instruction}";
        }
    }

    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class Ent
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("localizedName")]
        public string LocalizedName { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"{Id} - {Name}";
        }
    }

    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class Length
    {
        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"{Number}{Unit}";
        }
    }

    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class ExtendedIngredient
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("aisle")]
        public string Aisle { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        public string ImageSrc
        {
            get { return $"https://spoonacular.com/cdn/ingredients_100x100/{Image}"; }
        }

        [JsonProperty("consistency")]
        public string Consistency { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nameClean")]
        public string NameClean { get; set; }

        [JsonProperty("original")]
        public string Original { get; set; }

        [JsonProperty("originalString")]
        public string OriginalString { get; set; }

        [JsonProperty("originalName")]
        public string OriginalName { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        public string AmountString
        {
            get
            {
                return Amount.ToString("0.##");
            }
        }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("meta")]
        public string[] Meta { get; set; }

        [JsonProperty("metaInformation")]
        public string[] MetaInformation { get; set; }

        [JsonProperty("measures")]
        public Measures Measures { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"{Id} - {Name}";
        }
    }

    public partial class Measures
    {
        [JsonProperty("us")]
        public Metric Us { get; set; }

        [JsonProperty("metric")]
        public Metric Metric { get; set; }
    }

    public partial class Metric
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("unitShort")]
        public string UnitShort { get; set; }

        [JsonProperty("unitLong")]
        public string UnitLong { get; set; }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

}

