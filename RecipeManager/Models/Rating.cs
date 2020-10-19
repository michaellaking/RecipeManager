using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeManager.Models
{
    public class Rating
    {
        [Key]
        public long Id { get; set; }
        
        [Range(0, 5)]
        public int Stars { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        [ForeignKey("Recipe")]
        public long RecipeId { get; set; }



    }
}
