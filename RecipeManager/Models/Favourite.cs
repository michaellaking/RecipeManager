using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeManager.Models
{
    public class Favourite
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("Recipe")]
        public long RecipeId { get; set; }
        
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

    }
}
