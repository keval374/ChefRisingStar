using System;
using System.Collections.ObjectModel;
using SQLite;
using System.Collections.Generic;
using ChefRisingStar.DTOs;

namespace ChefRisingStar.Models
{
	public class CustomRecipe
	{
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        //[JsonProperty("title")]
        public string RecipeTitle { get; set; }

        //[JsonProperty("summary")]
        public string Summary { get; set; }

        //[JsonProperty("readyInMinutes")]
        public long ReadyInMinutes { get; set; }

        //[JsonProperty("servings")]
        public long Servings { get; set; }

        //[JsonProperty("instructions")]
        public string Instructions { get; set; }

        public string Cuisines { get; set; }
        //public List<string> Cuisines = new List<string>();
        //public List<string> DishTypes = new List<string>();
        public string DishTypes { get; set; }

        public CustomRecipe()
        {

        }

        public override string ToString()
        {
            return "[RecipeTitle:" + RecipeTitle +", Id:" +ID+" ]";
        }

        public static explicit operator CustomRecipeDto(CustomRecipe customrecipe)
        {
            var item = new CustomRecipeDto()
            {
                ID = customrecipe.ID,
                RecipeTitle = customrecipe.RecipeTitle,
                Summary = customrecipe.Summary,
                ReadyInMinutes = customrecipe.ReadyInMinutes,
                Servings = customrecipe.Servings,
                Instructions = customrecipe.Instructions,
                Cuisines = customrecipe.Cuisines,
                DishTypes = customrecipe.DishTypes
            };

            return item;
        }
    }
}

