using System;
using System.Collections.Generic;
using System.Text;

namespace ChefRisingStar.Models
{
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
}

