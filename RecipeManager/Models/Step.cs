using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeManager.Models
{
    public class Step
    {
        [Key]
        public long Id { get; set; }
        public int StepNumber { get; set; }
        public string Description { get; set; }

        [ForeignKey("Recipe")]
        public long RecipeId { get; set; }

        public Step(int stepNumber, string description)
        {
            StepNumber = stepNumber;
            Description = description;
        }
    }
}
