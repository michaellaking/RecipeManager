using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeManager.Models
{
    public class Ingredient
    {
        [Key]
        public long Id { get; set; }
        public string Measurement { get; set; }
        public string Name { get; set; }

        [ForeignKey("Recipe")]
        public long RecipeId { get; set; }

        public Ingredient(string measurement, string name)
        {
            Measurement = measurement;
            Name = name;
        }
    }
}
