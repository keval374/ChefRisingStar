using System;
using System.Collections.Generic;
using SQLite;

namespace ChefRisingStar.DTOs
{
	public partial class CustomRecipeDto
	{
        [PrimaryKey, AutoIncrement]
        public long ID { get; set; }

        public string RecipeTitle { get; set; }

        public string Summary { get; set; }

        public long ReadyInMinutes { get; set; }

        public long Servings { get; set; }

        public string Instructions { get; set; }

        public string Cuisines { get; set; }

        public string DishTypes { get; set; }

    }
}